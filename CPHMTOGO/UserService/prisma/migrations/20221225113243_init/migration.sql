/*
  Warnings:

  - Changed the type of `loginInfoId` on the `Company` table. No cast exists, the column would be dropped and recreated, which cannot be done if there is data, since the column is required.
  - Changed the type of `address` on the `Customer` table. No cast exists, the column would be dropped and recreated, which cannot be done if there is data, since the column is required.
  - Changed the type of `loginInfoId` on the `Customer` table. No cast exists, the column would be dropped and recreated, which cannot be done if there is data, since the column is required.
  - Added the required column `accountId` to the `Employee` table without a default value. This is not possible if the table is not empty.
  - Changed the type of `address` on the `Employee` table. No cast exists, the column would be dropped and recreated, which cannot be done if there is data, since the column is required.

*/
-- AlterTable
ALTER TABLE "Company" ALTER COLUMN "role" DROP DEFAULT,
DROP COLUMN "loginInfoId",
ADD COLUMN     "loginInfoId" UUID NOT NULL;

-- AlterTable
ALTER TABLE "Customer" DROP COLUMN "address",
ADD COLUMN     "address" UUID NOT NULL,
DROP COLUMN "loginInfoId",
ADD COLUMN     "loginInfoId" UUID NOT NULL,
ALTER COLUMN "role" DROP DEFAULT;

-- AlterTable
ALTER TABLE "Employee" ADD COLUMN     "accountId" TEXT NOT NULL,
DROP COLUMN "address",
ADD COLUMN     "address" UUID NOT NULL,
ALTER COLUMN "role" DROP DEFAULT;
