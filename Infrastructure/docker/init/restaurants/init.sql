CREATE TABLE
  public."MenuItems" (
    id uuid NOT NULL,
    "menuId" uuid NOT NULL,
    name text NOT NULL,
    description text NOT NULL,
    price double precision NOT NULL,
    "foodType" text NOT NULL
  );

ALTER TABLE
  public."MenuItems"
ADD
  CONSTRAINT "MenuItems_pkey" PRIMARY KEY (id);

CREATE TABLE
  public."Menu" (
    id uuid NOT NULL,
    title text NOT NULL,
    "restaurantId" uuid NOT NULL
  );

ALTER TABLE
  public."Menu"
ADD
  CONSTRAINT "Menu_pkey" PRIMARY KEY (id);

CREATE TABLE
  public."Restaurant" (
    id uuid NOT NULL,
    name text NOT NULL,
    "kontoNr" integer NOT NULL,
    "regNr" integer NOT NULL,
    role text NOT NULL,
    "accountId" text NOT NULL,
    cvr integer NOT NULL,
    address uuid NOT NULL,
    "loginInfoId" uuid NOT NULL
  );

ALTER TABLE
  public."Restaurant"
ADD
  CONSTRAINT "Restaurant_pkey" PRIMARY KEY (id);