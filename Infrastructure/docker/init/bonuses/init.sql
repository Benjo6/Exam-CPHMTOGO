CREATE TABLE
  public."Bonus" (
    id uuid NOT NULL,
    factor double precision NOT NULL,
    bonustype text NOT NULL
  );

ALTER TABLE
  public."Bonus"
ADD
  CONSTRAINT "Bonus_pkey" PRIMARY KEY (id)