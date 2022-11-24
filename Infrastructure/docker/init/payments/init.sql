CREATE TABLE
  public."Payment" (
    id uuid NOT NULL,
    "from" uuid NOT NULL,
    "to" uuid NOT NULL,
    amount double precision NOT NULL,
    type text NOT NULL
  );

ALTER TABLE
  public."Payment"
ADD
  CONSTRAINT "Payment_pkey" PRIMARY KEY (id)