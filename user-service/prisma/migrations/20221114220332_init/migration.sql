-- CreateTable
CREATE TABLE "Employee" (
    "id" UUID NOT NULL,
    "firstname" TEXT NOT NULL,
    "lastname" TEXT NOT NULL,
    "active" BOOLEAN NOT NULL,
    "loginInfoId" UUID NOT NULL,
    "address" TEXT NOT NULL,
    "kontoNr" INTEGER NOT NULL,
    "regNr" INTEGER NOT NULL,

    CONSTRAINT "Employee_pkey" PRIMARY KEY ("id")
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

    CONSTRAINT "Customer_pkey" PRIMARY KEY ("id")
);

-- CreateIndex
CREATE UNIQUE INDEX "Employee_id_key" ON "Employee"("id");

-- CreateIndex
CREATE UNIQUE INDEX "Customer_id_key" ON "Customer"("id");
