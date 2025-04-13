using System.Linq.Expressions;
using System.Reflection;

namespace SimpleEWallet.Comon.Extensions
{
	public static partial class IQueryable_Extensions
	{
		public static IQueryable<T> Paging<T>(this IQueryable<T> query, int pageNumber, int pageSize)
		{
			IQueryable<T> result = query;
			if (pageNumber < 0)
			{
				throw new Exception("Parameter pageNumber must be >= 0.");
			}
			if (pageSize < 0)
			{
				throw new Exception("Parameter pageSize must be >= 0.");
			}

			if (pageNumber == 0 || pageSize == 0)
			{
				return result;
			}

			int skip = (pageNumber - 1) * pageSize;
			result = result.Skip(skip).Take(pageSize);
			return result;
		}

		public static IQueryable<TSource> Sorting<TSource>(this IQueryable<TSource> query, string? orderByColumn, bool isAscending)
		{
			IQueryable<TSource> result = query;
			IQueryable<TSource>? resultQuery = null;
			if (orderByColumn.IsNullOrWhiteSpace())
			{
				throw new Exception("Order by must specify column name.");
			}

			ParameterExpression parameter = Expression.Parameter(typeof(TSource), "row");

			string[] orderByProps = orderByColumn.Split('.');
			Type sourceType = typeof(TSource);
			ParameterExpression arg = Expression.Parameter(sourceType, "row");
			Expression expr = arg;
			if (sourceType != null)
			{
				foreach (string prop in orderByProps)
				{
					// use reflection (not ComponentModel) to mirror LINQ
					PropertyInfo? pi = sourceType.GetProperty(prop, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.IgnoreCase);
					if (pi == null)
					{
						throw new Exception("Invalid Order By Column.");
					}
					else
					{
						expr = Expression.Property(expr, pi);
						sourceType = pi.PropertyType;
					}
				}

				Type delegateType = typeof(Func<,>).MakeGenericType(typeof(TSource), sourceType);
				LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

				string functionName = "OrderBy";
				if (!isAscending)
				{
					functionName = "OrderByDescending";
				}
				MethodInfo method = typeof(Queryable).GetMethods().Single(m => m.Name == functionName && m.GetParameters().Length == 2);
				MethodInfo genericMethod = method.MakeGenericMethod(new[] { typeof(TSource), sourceType });

				resultQuery = genericMethod.Invoke(query, new object[] { query, lambda }) as IOrderedQueryable<TSource>;
			}

			if (resultQuery == null)
			{
				throw new Exception("Order by " + orderByColumn + " fail.");
			}
			result = resultQuery;
			return result;
		}

		public static IQueryable<TSource> BuildQueryIsUnique<TSource, TUnique, TPrimaryKey>(this IQueryable<TSource> query, Expression<Func<TSource, TUnique>> expUniqueProperty, TUnique uniqueValue, Expression<Func<TSource, TPrimaryKey>> expPrimaryKeyProperty, TPrimaryKey? primaryKey)
		{
			IQueryable<TSource> result = query;
			ParameterExpression expEntity = Expression.Parameter(typeof(TSource));

			string uniquePropertyName = expUniqueProperty.GetPropertyName();
			MemberExpression expUniquePropertyForQuery = Expression.Property(expEntity, uniquePropertyName);

			ConstantExpression expUniqueValue = Expression.Constant(uniqueValue);
			BinaryExpression expWhereUnique = Expression.Equal(expUniquePropertyForQuery, expUniqueValue);

			if (typeof(TUnique) == typeof(string))
			{
				MethodInfo? methodInfo = typeof(string).GetMethod(nameof(string.ToUpper), Array.Empty<Type>());

				if (methodInfo == null)
				{
					throw new Exception("Method ToUpper not found.");
				}

				MethodCallExpression expUniquePropertyToUpper = Expression.Call(expUniquePropertyForQuery, methodInfo);
				expUniqueValue = Expression.Constant(uniqueValue?.ToString()?.ToUpper());
				expWhereUnique = Expression.Equal(expUniquePropertyToUpper, expUniqueValue);
			}

			Expression<Func<TSource, bool>> predicateUnique = Expression.Lambda<Func<TSource, bool>>(expWhereUnique, expEntity);
			result = result.Where(predicateUnique);

			if (primaryKey != null)
			{
				string primaryKeyPropertyName = expPrimaryKeyProperty.GetPropertyName();
				MemberExpression expPrimaryKeyPropertyForQuery = Expression.Property(expEntity, primaryKeyPropertyName);
				ConstantExpression expPrimaryKey = Expression.Constant(primaryKey);
				BinaryExpression expWherePrimaryKey = Expression.NotEqual(expPrimaryKeyPropertyForQuery, expPrimaryKey);
				Expression<Func<TSource, bool>> predicatePrimaryKey = Expression.Lambda<Func<TSource, bool>>(expWherePrimaryKey, expEntity);
				result = result.Where(predicatePrimaryKey);
			}
			return result;
		}

		public static string GetPropertyName(this LambdaExpression exp)
		{
			MemberExpression? expMember = exp.Body as MemberExpression;
			if (expMember == null)
			{
				if (exp.Body is UnaryExpression expUnary)
				{
					expMember = expUnary.Operand as MemberExpression;
				}
			}
			return expMember == null ? throw new Exception("Fail to get property name.") : expMember.Member.Name;
		}
	}
}
