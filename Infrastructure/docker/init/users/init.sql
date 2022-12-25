CREATE TABLE
  public."Company" (
    id uuid NOT NULL,
    name text NOT NULL,
    role text NOT NULL,
    "kontoNr" integer NOT NULL,
    "regNr" integer NOT NULL,
    "loginInfoId" uuid NOT NULL
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
    role text NOT NULL,
    address uuid NOT NULL,
    "loginInfoId" uuid NOT NULL
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
    role text NOT NULL,
    "kontoNr" integer NOT NULL,
    "regNr" integer NOT NULL,
    "accountId" text NOT NULL,
    address uuid NOT NULL
  );

ALTER TABLE
  public."Employee"
ADD
  CONSTRAINT "Employee_pkey" PRIMARY KEY (id);