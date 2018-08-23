-- SCHEMA: dbo

-- DROP SCHEMA dbo ;

CREATE SCHEMA dbo
    AUTHORIZATION postgres;
	
	
	
-- SEQUENCE: dbo.sec_curso_id

-- DROP SEQUENCE dbo.sec_curso_id;

CREATE SEQUENCE dbo.sec_curso_id;

ALTER SEQUENCE dbo.sec_curso_id
    OWNER TO postgres;

-- SEQUENCE: dbo.sec_inscricao_id

-- DROP SEQUENCE dbo.sec_inscricao_id;

CREATE SEQUENCE dbo.sec_inscricao_id;

ALTER SEQUENCE dbo.sec_inscricao_id
    OWNER TO postgres;
	
-- SEQUENCE: dbo.sec_pessoa_id

-- DROP SEQUENCE dbo.sec_pessoa_id;

CREATE SEQUENCE dbo.sec_pessoa_id;

ALTER SEQUENCE dbo.sec_pessoa_id
    OWNER TO postgres;
	


-- Table: dbo.cursos

-- DROP TABLE dbo.cursos;

CREATE TABLE dbo.cursos
(
    curso_id bigint NOT NULL DEFAULT nextval('dbo.sec_curso_id'::regclass),
    curso_nome text COLLATE pg_catalog."default" NOT NULL,
    data_inicio date,
    hora_inicio time without time zone,
    data_termino date,
    hora_termino time without time zone,
    descricao text COLLATE pg_catalog."default",
    CONSTRAINT cursos_pkey PRIMARY KEY (curso_id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE dbo.cursos
    OWNER to postgres;
	
	
	
-- Table: dbo.pessoas

-- DROP TABLE dbo.pessoas;

CREATE TABLE dbo.pessoas
(
    pessoa_id bigint NOT NULL DEFAULT nextval('dbo.sec_pessoa_id'::regclass),
    pessoa_nome text COLLATE pg_catalog."default" NOT NULL,
    data_nascimento date,
    sexo smallint,
    telefone_fixo text COLLATE pg_catalog."default",
    telefone_movel text COLLATE pg_catalog."default",
    observacoes text COLLATE pg_catalog."default",
    CONSTRAINT pessoas_pkey PRIMARY KEY (pessoa_id)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE dbo.pessoas
    OWNER to postgres;
	


-- Table: dbo.inscritos

-- DROP TABLE dbo.inscritos;

CREATE TABLE dbo.inscritos
(
    inscricao_id bigint NOT NULL DEFAULT nextval('dbo.sec_inscricao_id'::regclass),
    curso_id bigint NOT NULL,
    pessoa_id bigint NOT NULL,
    CONSTRAINT inscritos_curso_pkey PRIMARY KEY (inscricao_id),
    CONSTRAINT curso_id_fkey FOREIGN KEY (curso_id)
        REFERENCES dbo.cursos (curso_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT pessoa_id_fkey FOREIGN KEY (pessoa_id)
        REFERENCES dbo.pessoas (pessoa_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE dbo.inscritos
    OWNER to postgres;
	

