// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

#pragma warning disable Nix01 // Line is too long
#pragma warning disable SA1404 // Code analysis suppression must have justification
[assembly:
    SuppressMessage("DESIGN", "Nix02:Method is too long", Justification = "<Pending>", Scope = "member",
        Target = "~M:DAL.Migrations.InitialCreate.Up(Microsoft.EntityFrameworkCore.Migrations.MigrationBuilder)")]
#pragma warning restore SA1404 // Code analysis suppression must have justification
#pragma warning restore Nix01 // Line is too long