-- CreateTable
CREATE TABLE "Company" (
    "id" UUID NOT NULL,
    "name" TEXT NOT NULL,
    "role" TEXT NOT NULL DEFAULT 'Admin',
    "loginInfoId" TEXT NOT NULL,
    "kontoNr" INTEGER NOT NULL,
    "regNr" INTEGER NOT NULL,

    CONSTRAINT "Company_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "Customer" (
    "id" UUID NOT NULL,
    "firstname" TEXT NOT NULL,
    "lastname" TEXT NOT NULL,
    "phone" INTEGER NOT NULL,
    "birtdate" TIMESTAMP(3) NOT NULL,
    "address" TEXT NOT NULL,
    "loginInfoId" TEXT NOT NULL,
    "role" TEXT NOT NULL DEFAULT 'Customer',

    CONSTRAINT "Customer_pkey" PRIMARY KEY ("id")
);

-- CreateTable
CREATE TABLE "Employee" (
    "id" UUID NOT NULL,
    "firstname" TEXT NOT NULL,
    "lastname" TEXT NOT NULL,
    "active" BOOLEAN NOT NULL,
    "loginInfoId" UUID NOT NULL,
    "address" TEXT NOT NULL,
    "role" TEXT NOT NULL DEFAULT 'Employee',
    "kontoNr" INTEGER NOT NULL,
    "regNr" INTEGER NOT NULL,

    CONSTRAINT "Employee_pkey" PRIMARY KEY ("id")
);
