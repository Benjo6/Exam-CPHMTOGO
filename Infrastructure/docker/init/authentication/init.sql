CREATE TABLE
  public."LoginInfo" (
    id uuid NOT NULL,
    username text NOT NULL,
    "passwordHash" bytea NOT NULL,
    salt bytea NOT NULL,
    email text NOT NULL
  );

ALTER TABLE
  public."LoginInfo"
ADD
  CONSTRAINT "LoginInfo_pkey" PRIMARY KEY (id)