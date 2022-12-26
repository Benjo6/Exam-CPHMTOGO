CREATE TABLE
  public."Address" (
    id uuid NOT NULL,
    street text NOT NULL,
    "streetNr" text NOT NULL,
    zipcode text NOT NULL,
    longitude float8 NOT NULL,
    latitude float8 NOT NULL,
    etage text,
    door text
  );

ALTER TABLE
  public."Address"
ADD
  CONSTRAINT "Address_pkey" PRIMARY KEY (id);