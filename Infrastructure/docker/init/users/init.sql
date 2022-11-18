CREATE TABLE
  public."Company" (
    id uuid NOT NULL,
    name text NOT NULL,
    role text NOT NULL DEFAULT 'Admin' ::text,
    "loginInfoId" text NOT NULL,
    "kontoNr" integer NOT NULL,
    "regNr" integer NOT NULL
  );

ALTER TABLE
  public."Company"
ADD
  CONSTRAINT "Company_pkey" PRIMARY KEY (id);

CREATE TABLE
  public."Customer" (
    id uuid NOT NULL,
    firstname text NOT NULL,
    lastname text NOT NULL,
    phone integer NOT NULL,
    birtdate timestamp(3) without time zone NOT NULL,
    address text NOT NULL,
    "loginInfoId" text NOT NULL,
    role text NOT NULL DEFAULT 'Customer' ::text
  );

ALTER TABLE
  public."Customer"
ADD
  CONSTRAINT "Customer_pkey" PRIMARY KEY (id);

CREATE TABLE
  public."Employee" (
    id uuid NOT NULL,
    firstname text NOT NULL,
    lastname text NOT NULL,
    active boolean NOT NULL,
    "loginInfoId" uuid NOT NULL,
    address text NOT NULL,
    role text NOT NULL DEFAULT 'Employee' ::text,
    "kontoNr" integer NOT NULL,
    "regNr" integer NOT NULL
  );

ALTER TABLE
  public."Employee"
ADD
  CONSTRAINT "Employee_pkey" PRIMARY KEY (id)