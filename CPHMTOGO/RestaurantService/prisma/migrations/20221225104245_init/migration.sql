/*
  Warnings:

  - You are about to drop the column `CVR` on the `Restaurant` table. All the data in the column will be lost.
  - You are about to drop the column `cityId` on the `Restaurant` table. All the data in the column will be lost.
  - Added the required column `accountId` to the `Restaurant` table without a default value. This is not possible if the table is not empty.
  - Added the required column `cvr` to the `Restaurant` table without a default value. This is not possible if the table is not empty.
  - Changed the type of `address` on the `Restaurant` table. No cast exists, the column would be dropped and recreated, which cannot be done if there is data, since the column is required.
  - Changed the type of `loginInfoId` on the `Restaurant` table. No cast exists, the column would be dropped and recreated, which cannot be done if there is data, since the column is required.

*/
-- AlterTable
ALTER TABLE "Restaurant" DROP COLUMN "CVR",
DROP COLUMN "cityId",
ADD COLUMN     "accountId" TEXT NOT NULL,
ADD COLUMN     "cvr" INTEGER NOT NULL,
DROP COLUMN "address",
ADD COLUMN     "address" UUID NOT NULL,
DROP COLUMN "loginInfoId",
ADD COLUMN     "loginInfoId" UUID NOT NULL,
ALTER COLUMN "role" DROP DEFAULT;
