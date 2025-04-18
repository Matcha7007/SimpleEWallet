--
-- PostgreSQL database dump
--

-- Dumped from database version 16.3
-- Dumped by pg_dump version 17.2

-- Started on 2025-04-15 13:58:34

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 217 (class 1259 OID 41958)
-- Name: mst_config; Type: TABLE; Schema: public; Owner: admin_db
--

CREATE TABLE public.mst_config (
    id integer NOT NULL,
    config_key character varying(500) NOT NULL,
    config_value text,
    is_active boolean DEFAULT true,
    created_by uuid,
    created_at time without time zone DEFAULT now(),
    last_modified_by uuid,
    last_modified_at time without time zone DEFAULT now()
);


ALTER TABLE public.mst_config OWNER TO admin_db;

--
-- TOC entry 216 (class 1259 OID 41957)
-- Name: mst_config_id_seq; Type: SEQUENCE; Schema: public; Owner: admin_db
--

ALTER TABLE public.mst_config ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.mst_config_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    MAXVALUE 9999999
    CACHE 1
    CYCLE
);


--
-- TOC entry 215 (class 1259 OID 41851)
-- Name: mst_user; Type: TABLE; Schema: public; Owner: admin_db
--

CREATE TABLE public.mst_user (
    id uuid DEFAULT gen_random_uuid() NOT NULL,
    username character varying(50) NOT NULL,
    full_name character varying(100) NOT NULL,
    email character varying(50) NOT NULL,
    phone character varying(14) NOT NULL,
    password_hash text NOT NULL,
    pin_hash text,
    is_active boolean DEFAULT true,
    created_by uuid,
    created_at timestamp without time zone DEFAULT now(),
    last_modified_by uuid,
    last_modified_at timestamp without time zone DEFAULT now()
);


ALTER TABLE public.mst_user OWNER TO admin_db;

--
-- TOC entry 4850 (class 0 OID 41958)
-- Dependencies: 217
-- Data for Name: mst_config; Type: TABLE DATA; Schema: public; Owner: admin_db
--

COPY public.mst_config (id, config_key, config_value, is_active, created_by, created_at, last_modified_by, last_modified_at) FROM stdin;
\.


--
-- TOC entry 4848 (class 0 OID 41851)
-- Dependencies: 215
-- Data for Name: mst_user; Type: TABLE DATA; Schema: public; Owner: admin_db
--

COPY public.mst_user (id, username, full_name, email, phone, password_hash, pin_hash, is_active, created_by, created_at, last_modified_by, last_modified_at) FROM stdin;
004ca24e-9a38-4104-8514-27e68133f013	Similikiti	Wallet Tester	wallet@gmail.com	0812102663004	79FBFAC39441E21D063C4036112E511716F8B165673A4633872A739FF990C2D1:A459B2F7078F334E52104409473D4659:50000:SHA256	9A558DEEC33AF1E615C1AC605131C7E665961F5F97DFE2A5416FF965AC14F186:E574AECD429DEC3656DD7D468CB28DBA:50000:SHA256	t	\N	2025-04-14 08:39:24.508815	\N	2025-04-14 08:39:24.508928
\.


--
-- TOC entry 4856 (class 0 OID 0)
-- Dependencies: 216
-- Name: mst_config_id_seq; Type: SEQUENCE SET; Schema: public; Owner: admin_db
--

SELECT pg_catalog.setval('public.mst_config_id_seq', 1, false);


--
-- TOC entry 4704 (class 2606 OID 41967)
-- Name: mst_config mst_config_pkey; Type: CONSTRAINT; Schema: public; Owner: admin_db
--

ALTER TABLE ONLY public.mst_config
    ADD CONSTRAINT mst_config_pkey PRIMARY KEY (id);


--
-- TOC entry 4700 (class 2606 OID 41861)
-- Name: mst_user mst_user_pkey; Type: CONSTRAINT; Schema: public; Owner: admin_db
--

ALTER TABLE ONLY public.mst_user
    ADD CONSTRAINT mst_user_pkey PRIMARY KEY (id);


--
-- TOC entry 4702 (class 2606 OID 41863)
-- Name: mst_user unique; Type: CONSTRAINT; Schema: public; Owner: admin_db
--

ALTER TABLE ONLY public.mst_user
    ADD CONSTRAINT "unique" UNIQUE (email, phone);


-- Completed on 2025-04-15 13:58:34

--
-- PostgreSQL database dump complete
--

