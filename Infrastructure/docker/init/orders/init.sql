CREATE TABLE
  public."OrdreStatus" (
    id uuid NOT NULL,
    "timeStamp" timestamp(3) without time zone NOT NULL,
    status text NOT NULL
  );

ALTER TABLE
  public."OrdreStatus"
ADD
  CONSTRAINT "OrdreStatus_pkey" PRIMARY KEY (id);

CREATE TABLE
  public."OrderItem" (
    id uuid NOT NULL,
    preference text NOT NULL,
    price float8 NOT NULL,
    quantity integer NOT NULL,
    "orderId" uuid NOT NULL
  );

ALTER TABLE
  public."OrderItem"
ADD
  CONSTRAINT "OrderItem_pkey" PRIMARY KEY (id);

CREATE TABLE
  public."Order" (
    id uuid NOT NULL,
    "customerId" uuid NOT NULL,
    "employeeId" uuid,
    "restaurantId" uuid NOT NULL,
    "addressId" uuid NOT NULL,
    "ordreStatusId" uuid NOT NULL
  );

ALTER TABLE
  public."Order"
ADD
  CONSTRAINT "Order_pkey" PRIMARY KEY (id);

CREATE TABLE
  public."Receipt" (
    id uuid NOT NULL,
    amount float8 NOT NULL,
    "time" timestamp(3) without time zone NOT NULL,
    "orderId" uuid NOT NULL
  );

ALTER TABLE
  public."Receipt"
ADD
  CONSTRAINT "Receipt_pkey" PRIMARY KEY (id);