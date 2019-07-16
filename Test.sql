CREATE SEQUENCE person_name_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
	
CREATE SEQUENCE person_surname_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;

CREATE TABLE person_name (
    id bigint PRIMARY KEY,
    name character varying(255),
    city character varying(255)
);

ALTER TABLE ONLY person_name ALTER COLUMN id SET DEFAULT nextval('person_name_seq'::regclass);

CREATE TABLE person_surname (
    id bigint PRIMARY KEY,
	surname character varying(255)
);

ALTER TABLE ONLY person_surname ALTER COLUMN id SET DEFAULT nextval('person_surname_seq'::regclass);


CREATE TABLE person_conn (
    name_id bigint NOT NULL,
    surname_id bigint NOT NULL
);

ALTER TABLE ONLY person_conn
    ADD CONSTRAINT person_conn_pkey PRIMARY KEY (name_id, surname_id);

ALTER TABLE person_conn 
ADD CONSTRAINT fk_name_id FOREIGN KEY (name_id) REFERENCES person_name(id) ON DELETE CASCADE;

ALTER TABLE person_conn 
ADD CONSTRAINT fk_surname_id FOREIGN KEY (surname_id) REFERENCES person_surname(id) ON DELETE CASCADE;

INSERT INTO person_name(name, city)
VALUES
('c', 'Prague'),
('b', 'London'),
('a', 'London');

INSERT INTO person_surname(surname)
VALUES
('aa'),
('bb'),
('c');

INSERT INTO person_conn(name_id, surname_id)
VALUES
(1, 1),
(2, 2),
(3, 3);