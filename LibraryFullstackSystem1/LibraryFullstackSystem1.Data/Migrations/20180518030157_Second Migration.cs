﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace LibraryFullstackSystem1.Data.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patrons_LibraryBranches_HomeLibraryBranchId",
                table: "Patrons");

            migrationBuilder.DropForeignKey(
                name: "FK_Patrons_LibraryCards_LibrayCardId",
                table: "Patrons");

            migrationBuilder.RenameColumn(
                name: "LibrayCardId",
                table: "Patrons",
                newName: "LibraryCardIdId");

            migrationBuilder.RenameColumn(
                name: "HomeLibraryBranchId",
                table: "Patrons",
                newName: "HomeLibraryBranchIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Patrons_LibrayCardId",
                table: "Patrons",
                newName: "IX_Patrons_LibraryCardIdId");

            migrationBuilder.RenameIndex(
                name: "IX_Patrons_HomeLibraryBranchId",
                table: "Patrons",
                newName: "IX_Patrons_HomeLibraryBranchIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patrons_LibraryBranches_HomeLibraryBranchIdId",
                table: "Patrons",
                column: "HomeLibraryBranchIdId",
                principalTable: "LibraryBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patrons_LibraryCards_LibraryCardIdId",
                table: "Patrons",
                column: "LibraryCardIdId",
                principalTable: "LibraryCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patrons_LibraryBranches_HomeLibraryBranchIdId",
                table: "Patrons");

            migrationBuilder.DropForeignKey(
                name: "FK_Patrons_LibraryCards_LibraryCardIdId",
                table: "Patrons");

            migrationBuilder.RenameColumn(
                name: "LibraryCardIdId",
                table: "Patrons",
                newName: "LibrayCardId");

            migrationBuilder.RenameColumn(
                name: "HomeLibraryBranchIdId",
                table: "Patrons",
                newName: "HomeLibraryBranchId");

            migrationBuilder.RenameIndex(
                name: "IX_Patrons_LibraryCardIdId",
                table: "Patrons",
                newName: "IX_Patrons_LibrayCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Patrons_HomeLibraryBranchIdId",
                table: "Patrons",
                newName: "IX_Patrons_HomeLibraryBranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patrons_LibraryBranches_HomeLibraryBranchId",
                table: "Patrons",
                column: "HomeLibraryBranchId",
                principalTable: "LibraryBranches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Patrons_LibraryCards_LibrayCardId",
                table: "Patrons",
                column: "LibrayCardId",
                principalTable: "LibraryCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
