PGDMP      ,                |            event_booking_db    17.2    17.2     �           0    0    ENCODING    ENCODING     #   SET client_encoding = 'SQL_ASCII';
                           false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            �           1262    16406    event_booking_db    DATABASE     �   CREATE DATABASE event_booking_db WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Russian_Russia.1251';
     DROP DATABASE event_booking_db;
                     event_backend    false                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
                     pg_database_owner    false            �           0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                        pg_database_owner    false    4            �            1259    16412    Users    TABLE     �   CREATE TABLE public."Users" (
    "Id" uuid NOT NULL,
    "Username" text NOT NULL,
    "Password" text NOT NULL,
    "Email" text NOT NULL
);
    DROP TABLE public."Users";
       public         heap r       event_backend    false    4            �            1259    16407    __EFMigrationsHistory    TABLE     �   CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);
 +   DROP TABLE public."__EFMigrationsHistory";
       public         heap r       event_backend    false    4            �          0    16412    Users 
   TABLE DATA           H   COPY public."Users" ("Id", "Username", "Password", "Email") FROM stdin;
    public               event_backend    false    218   (       �          0    16407    __EFMigrationsHistory 
   TABLE DATA           R   COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
    public               event_backend    false    217   �       '           2606    16418    Users PK_Users 
   CONSTRAINT     R   ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "PK_Users" PRIMARY KEY ("Id");
 <   ALTER TABLE ONLY public."Users" DROP CONSTRAINT "PK_Users";
       public                 event_backend    false    218            %           2606    16411 .   __EFMigrationsHistory PK___EFMigrationsHistory 
   CONSTRAINT     {   ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");
 \   ALTER TABLE ONLY public."__EFMigrationsHistory" DROP CONSTRAINT "PK___EFMigrationsHistory";
       public                 event_backend    false    217            �   �   x�M�A
� ��._�k��z�n^�-!��T�z�
%Px�Y���x3Od�er�z��#Ң0�qt�*ξ��Tq�v��P�>*n�k��؅�U9�D���0A�e���N�Ð2>��Đ���鏚J)��ݥ�\T5!      �   0   x�320214204�0534����,�L�q.JM,I��3�3������ �	�     