CREATE TABLE
  public."Address" (
    id uuid NOT NULL,
    street text NOT NULL,
    "streetNr" text NOT NULL,
    zipcode text NOT NULL,
    longitude double precision NOT NULL,
    latitude double precision NOT NULL,
    "cityId" uuid NOT NULL
  );

ALTER TABLE
  public."Address"
ADD
  CONSTRAINT "Address_pkey" PRIMARY KEY (id)