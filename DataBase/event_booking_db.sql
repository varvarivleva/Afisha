--
-- PostgreSQL database dump
--

-- Dumped from database version 17.2
-- Dumped by pg_dump version 17.2

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
-- Name: Bookings; Type: TABLE; Schema: public; Owner: event_backend
--

CREATE TABLE public."Bookings" (
    "Id" uuid NOT NULL,
    "UserId" uuid NOT NULL,
    "EventId" uuid NOT NULL,
    "BookingTime" timestamp with time zone NOT NULL,
    "Status" text NOT NULL,
    "FirstName" text NOT NULL,
    "LastName" text NOT NULL,
    "Phone" text NOT NULL
);


ALTER TABLE public."Bookings" OWNER TO event_backend;

--
-- Name: Events; Type: TABLE; Schema: public; Owner: event_backend
--

CREATE TABLE public."Events" (
    "Id" uuid NOT NULL,
    "OrganizatorId" uuid NOT NULL,
    "Title" text NOT NULL,
    "Description" text NOT NULL,
    "EventTime" timestamp with time zone NOT NULL,
    "Venue" text NOT NULL,
    "TicketsAvailable" integer NOT NULL,
    "TicketPrice" numeric NOT NULL
);


ALTER TABLE public."Events" OWNER TO event_backend;

--
-- Name: Users; Type: TABLE; Schema: public; Owner: event_backend
--

CREATE TABLE public."Users" (
    "Id" uuid NOT NULL,
    "Username" text NOT NULL,
    "Password" text NOT NULL,
    "Email" text NOT NULL
);


ALTER TABLE public."Users" OWNER TO event_backend;

--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: event_backend
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO event_backend;

--
-- Data for Name: Bookings; Type: TABLE DATA; Schema: public; Owner: event_backend
--

COPY public."Bookings" ("Id", "UserId", "EventId", "BookingTime", "Status", "FirstName", "LastName", "Phone") FROM stdin;
c03bbe00-da78-4d8f-8bb8-a48844343591	019383b6-3442-7448-b0fc-d0abc557da38	c8fc15dc-bec7-4094-bab0-ab83711d152d	2024-12-22 20:40:40.896047+03	success	string	string	string
\.


--
-- Data for Name: Events; Type: TABLE DATA; Schema: public; Owner: event_backend
--

COPY public."Events" ("Id", "OrganizatorId", "Title", "Description", "EventTime", "Venue", "TicketsAvailable", "TicketPrice") FROM stdin;
e6114be6-88b9-4b92-b617-98e2d08977c3	019383b6-3442-7448-b0fc-d0abc557da38	Crazy event 2	This is super duper crazy mazy event for really cool people	2025-01-01 22:00:00+03	Really cool place	100	10
4e54cc3d-522d-4820-bbd7-8f9734de2569	00000000-0000-0000-0000-000000000000	Crazy Event 3	Another super mega crazy pazy GOAT event	2024-12-23 13:00:00+03	Cool Place	100	100
0720f2f6-dcba-4427-896d-b73fc0095101	00000000-0000-0000-0000-000000000000	Crazy Event 4	You want it? You get it!	2025-03-02 21:00:00+03	Cool Place	100	100
066c1b5f-3481-43e1-9937-8de9e981bc11	019383b6-3442-7448-b0fc-d0abc557da38	string	string	2024-12-23 20:31:55.244+03	string	10	10
c8fc15dc-bec7-4094-bab0-ab83711d152d	019383b6-3442-7448-b0fc-d0abc557da38	Crazy event	This is super duper crazy mazy event for really cool people	2024-12-31 22:00:00+03	Really cool place	99	10
\.


--
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: event_backend
--

COPY public."Users" ("Id", "Username", "Password", "Email") FROM stdin;
019383b6-3442-7448-b0fc-d0abc557da38	varvarivleva	varvarikivleva	v.katasova@gmail.com
019383b9-7a4e-72c8-96a1-42281231e329	elayzandra	lizzza	elizzavetttaa@gmail.com
dfccb480-dd88-4356-9f35-48b266e7ae39	new_user	user_password	user@mail.ru
\.


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: event_backend
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20241201185612_InitialCreate	9.0.0
20241215154718_AddEventsTable	9.0.0
\.


--
-- Name: Bookings PK_Bookings; Type: CONSTRAINT; Schema: public; Owner: event_backend
--

ALTER TABLE ONLY public."Bookings"
    ADD CONSTRAINT "PK_Bookings" PRIMARY KEY ("Id");


--
-- Name: Events PK_Events; Type: CONSTRAINT; Schema: public; Owner: event_backend
--

ALTER TABLE ONLY public."Events"
    ADD CONSTRAINT "PK_Events" PRIMARY KEY ("Id");


--
-- Name: Users PK_Users; Type: CONSTRAINT; Schema: public; Owner: event_backend
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "PK_Users" PRIMARY KEY ("Id");


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: event_backend
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: IX_Bookings_EventId; Type: INDEX; Schema: public; Owner: event_backend
--

CREATE INDEX "IX_Bookings_EventId" ON public."Bookings" USING btree ("EventId");


--
-- Name: Bookings FK_Bookings_Events_EventId; Type: FK CONSTRAINT; Schema: public; Owner: event_backend
--

ALTER TABLE ONLY public."Bookings"
    ADD CONSTRAINT "FK_Bookings_Events_EventId" FOREIGN KEY ("EventId") REFERENCES public."Events"("Id") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

