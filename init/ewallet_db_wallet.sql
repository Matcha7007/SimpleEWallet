--
-- PostgreSQL database dump
--

-- Dumped from database version 16.3
-- Dumped by pg_dump version 17.2

-- Started on 2025-04-15 13:59:23

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
-- TOC entry 221 (class 1259 OID 41970)
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
-- TOC entry 220 (class 1259 OID 41969)
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
-- TOC entry 217 (class 1259 OID 41875)
-- Name: mst_status; Type: TABLE; Schema: public; Owner: admin_db
--

CREATE TABLE public.mst_status (
    id integer NOT NULL,
    name character varying(25) NOT NULL,
    description text,
    is_active boolean DEFAULT true,
    created_by uuid,
    created_at timestamp without time zone DEFAULT now(),
    last_modified_by uuid,
    last_modified_at timestamp without time zone DEFAULT now()
);


ALTER TABLE public.mst_status OWNER TO admin_db;

--
-- TOC entry 216 (class 1259 OID 41874)
-- Name: mst_status_id_seq; Type: SEQUENCE; Schema: public; Owner: admin_db
--

ALTER TABLE public.mst_status ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.mst_status_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    MAXVALUE 9
    CACHE 1
    CYCLE
);


--
-- TOC entry 215 (class 1259 OID 41864)
-- Name: mst_wallet; Type: TABLE; Schema: public; Owner: admin_db
--

CREATE TABLE public.mst_wallet (
    id uuid DEFAULT gen_random_uuid() NOT NULL,
    user_id uuid NOT NULL,
    balance money DEFAULT 0 NOT NULL,
    is_active boolean DEFAULT true,
    created_by uuid,
    created_at timestamp without time zone DEFAULT now(),
    last_modified_by uuid,
    last_modified_at timestamp without time zone DEFAULT now(),
    wallet_number character varying(14) NOT NULL,
    wallet_name character varying(100) NOT NULL
);


ALTER TABLE public.mst_wallet OWNER TO admin_db;

--
-- TOC entry 218 (class 1259 OID 41885)
-- Name: trn_topup_request; Type: TABLE; Schema: public; Owner: admin_db
--

CREATE TABLE public.trn_topup_request (
    id uuid DEFAULT gen_random_uuid() NOT NULL,
    wallet_id uuid NOT NULL,
    amount money DEFAULT 0 NOT NULL,
    status_id integer DEFAULT 1,
    is_active boolean DEFAULT true,
    created_by uuid,
    created_at timestamp without time zone DEFAULT now(),
    last_modified_by uuid,
    last_modified_at timestamp without time zone DEFAULT now(),
    transaction_id uuid
);


ALTER TABLE public.trn_topup_request OWNER TO admin_db;

--
-- TOC entry 219 (class 1259 OID 41906)
-- Name: trn_transfer; Type: TABLE; Schema: public; Owner: admin_db
--

CREATE TABLE public.trn_transfer (
    id uuid DEFAULT gen_random_uuid() NOT NULL,
    sender_wallet_id uuid NOT NULL,
    receiver_wallet_id uuid NOT NULL,
    amount money NOT NULL,
    description text,
    status_id integer DEFAULT 1,
    is_active boolean DEFAULT true,
    created_by uuid,
    created_at timestamp without time zone DEFAULT now(),
    last_modified_by uuid,
    last_modified_at timestamp without time zone DEFAULT now(),
    transaction_id uuid
);


ALTER TABLE public.trn_transfer OWNER TO admin_db;

--
-- TOC entry 4891 (class 0 OID 41970)
-- Dependencies: 221
-- Data for Name: mst_config; Type: TABLE DATA; Schema: public; Owner: admin_db
--

COPY public.mst_config (id, config_key, config_value, is_active, created_by, created_at, last_modified_by, last_modified_at) FROM stdin;
1	UrlClaimToken	http://simpleewallet-auth-service-1:7095/api/v1/Auth/claim-token	t	\N	17:13:54.915526	\N	17:13:54.915526
2	UrlVerifyPin	http://simpleewallet-auth-service-1:7095/api/v1/User/verify-pin	t	\N	17:13:54.915526	\N	17:13:54.915526
\.


--
-- TOC entry 4887 (class 0 OID 41875)
-- Dependencies: 217
-- Data for Name: mst_status; Type: TABLE DATA; Schema: public; Owner: admin_db
--

COPY public.mst_status (id, name, description, is_active, created_by, created_at, last_modified_by, last_modified_at) FROM stdin;
1	Pending	Status Pending	t	\N	2025-04-12 20:37:25.806512	\N	2025-04-12 20:37:25.806512
2	Success	Status Success	t	\N	2025-04-12 20:37:38.974806	\N	2025-04-12 20:37:38.974806
3	Failed	Status Failed	t	\N	2025-04-12 20:37:57.502651	\N	2025-04-12 20:37:57.502651
\.


--
-- TOC entry 4885 (class 0 OID 41864)
-- Dependencies: 215
-- Data for Name: mst_wallet; Type: TABLE DATA; Schema: public; Owner: admin_db
--

COPY public.mst_wallet (id, user_id, balance, is_active, created_by, created_at, last_modified_by, last_modified_at, wallet_number, wallet_name) FROM stdin;
1add0b7e-a3b4-44a8-9f8b-652ee9cbf2e6	004ca24e-9a38-4104-8514-27e68133f013	$19,000.00	t	004ca24e-9a38-4104-8514-27e68133f013	2025-04-14 08:39:27.506988	004ca24e-9a38-4104-8514-27e68133f013	2025-04-14 15:00:16.200183	0812102663004	Wallet Tester
\.


--
-- TOC entry 4888 (class 0 OID 41885)
-- Dependencies: 218
-- Data for Name: trn_topup_request; Type: TABLE DATA; Schema: public; Owner: admin_db
--

COPY public.trn_topup_request (id, wallet_id, amount, status_id, is_active, created_by, created_at, last_modified_by, last_modified_at, transaction_id) FROM stdin;
4f865e29-41e9-4319-b6d5-98e67737a444	1add0b7e-a3b4-44a8-9f8b-652ee9cbf2e6	$19,000.00	2	t	004ca24e-9a38-4104-8514-27e68133f013	2025-04-14 15:00:01.310767	004ca24e-9a38-4104-8514-27e68133f013	2025-04-14 15:00:01.388765	d909cbac-422d-4199-aa67-fd99da517ba1
\.


--
-- TOC entry 4889 (class 0 OID 41906)
-- Dependencies: 219
-- Data for Name: trn_transfer; Type: TABLE DATA; Schema: public; Owner: admin_db
--

COPY public.trn_transfer (id, sender_wallet_id, receiver_wallet_id, amount, description, status_id, is_active, created_by, created_at, last_modified_by, last_modified_at, transaction_id) FROM stdin;
\.


--
-- TOC entry 4897 (class 0 OID 0)
-- Dependencies: 220
-- Name: mst_config_id_seq; Type: SEQUENCE SET; Schema: public; Owner: admin_db
--

SELECT pg_catalog.setval('public.mst_config_id_seq', 2, true);


--
-- TOC entry 4898 (class 0 OID 0)
-- Dependencies: 216
-- Name: mst_status_id_seq; Type: SEQUENCE SET; Schema: public; Owner: admin_db
--

SELECT pg_catalog.setval('public.mst_status_id_seq', 3, true);


--
-- TOC entry 4736 (class 2606 OID 41979)
-- Name: mst_config mst_config_pkey; Type: CONSTRAINT; Schema: public; Owner: admin_db
--

ALTER TABLE ONLY public.mst_config
    ADD CONSTRAINT mst_config_pkey PRIMARY KEY (id);


--
-- TOC entry 4730 (class 2606 OID 41884)
-- Name: mst_status mst_status_pkey; Type: CONSTRAINT; Schema: public; Owner: admin_db
--

ALTER TABLE ONLY public.mst_status
    ADD CONSTRAINT mst_status_pkey PRIMARY KEY (id);


--
-- TOC entry 4728 (class 2606 OID 41873)
-- Name: mst_wallet mst_wallet_pkey; Type: CONSTRAINT; Schema: public; Owner: admin_db
--

ALTER TABLE ONLY public.mst_wallet
    ADD CONSTRAINT mst_wallet_pkey PRIMARY KEY (id);


--
-- TOC entry 4732 (class 2606 OID 41895)
-- Name: trn_topup_request trn_topup_request_pkey; Type: CONSTRAINT; Schema: public; Owner: admin_db
--

ALTER TABLE ONLY public.trn_topup_request
    ADD CONSTRAINT trn_topup_request_pkey PRIMARY KEY (id);


--
-- TOC entry 4734 (class 2606 OID 41917)
-- Name: trn_transfer trn_transfer_pkey; Type: CONSTRAINT; Schema: public; Owner: admin_db
--

ALTER TABLE ONLY public.trn_transfer
    ADD CONSTRAINT trn_transfer_pkey PRIMARY KEY (id);


--
-- TOC entry 4739 (class 2606 OID 41923)
-- Name: trn_transfer fk_receiver_wallet_id; Type: FK CONSTRAINT; Schema: public; Owner: admin_db
--

ALTER TABLE ONLY public.trn_transfer
    ADD CONSTRAINT fk_receiver_wallet_id FOREIGN KEY (receiver_wallet_id) REFERENCES public.mst_wallet(id);


--
-- TOC entry 4740 (class 2606 OID 41918)
-- Name: trn_transfer fk_sender_wallet_id; Type: FK CONSTRAINT; Schema: public; Owner: admin_db
--

ALTER TABLE ONLY public.trn_transfer
    ADD CONSTRAINT fk_sender_wallet_id FOREIGN KEY (sender_wallet_id) REFERENCES public.mst_wallet(id);


--
-- TOC entry 4737 (class 2606 OID 41896)
-- Name: trn_topup_request fk_status_id; Type: FK CONSTRAINT; Schema: public; Owner: admin_db
--

ALTER TABLE ONLY public.trn_topup_request
    ADD CONSTRAINT fk_status_id FOREIGN KEY (status_id) REFERENCES public.mst_status(id);


--
-- TOC entry 4741 (class 2606 OID 41928)
-- Name: trn_transfer fk_status_id; Type: FK CONSTRAINT; Schema: public; Owner: admin_db
--

ALTER TABLE ONLY public.trn_transfer
    ADD CONSTRAINT fk_status_id FOREIGN KEY (status_id) REFERENCES public.mst_status(id);


--
-- TOC entry 4738 (class 2606 OID 41901)
-- Name: trn_topup_request fk_wallet_id; Type: FK CONSTRAINT; Schema: public; Owner: admin_db
--

ALTER TABLE ONLY public.trn_topup_request
    ADD CONSTRAINT fk_wallet_id FOREIGN KEY (wallet_id) REFERENCES public.mst_wallet(id);


-- Completed on 2025-04-15 13:59:23

--
-- PostgreSQL database dump complete
--

