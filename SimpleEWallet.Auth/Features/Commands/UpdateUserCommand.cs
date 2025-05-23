﻿using MediatR;

using SimpleEWallet.Comon.Models.Auth;

namespace SimpleEWallet.Auth.Features.Commands
{
	public record UpdateUserCommand(UserParameters Parameters) : IRequest<UserResponse>;
}
