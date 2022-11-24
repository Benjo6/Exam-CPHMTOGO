CREATE TABLE
  public."MenuItems" (
    id uuid NOT NULL,
    name text NOT NULL,
    description text NOT NULL,
    price text NOT NULL,
    "menuId" uuid NOT NULL,
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
    adress text NOT NULL,
    "loginInfoId" text NOT NULL,
    "cityId" text NOT NULL,
    "kontoNr" integer NOT NULL,
    "regNr" integer NOT NULL,
    "CVR" integer NOT NULL,
    role text NOT NULL DEFAULT 'Restaurant' ::text
  );

ALTER TABLE
  public."Restaurant"
ADD
  CONSTRAINT "Restaurant_pkey" PRIMARY KEY (id)