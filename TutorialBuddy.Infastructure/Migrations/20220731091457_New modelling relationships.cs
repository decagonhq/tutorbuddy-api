using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TutorBuddy.Infrastructure.Migrations
{
    public partial class Newmodellingrelationships : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    AvatarUrl = table.Column<string>(type: "text", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ImageMeta",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    PublicId = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Tag = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDepricated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageMeta", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AreaOfInterests",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDepricated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaOfInterests", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AreaOfInterests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reminders",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDepricated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Reminders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tutor",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    BioNote = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDepricated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tutor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tutor_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Availabilities",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    TutorID = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDepricated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availabilities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Availabilities_Tutor_TutorID",
                        column: x => x.TutorID,
                        principalTable: "Tutor",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TutorSubjects",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    TutorID = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDepricated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorSubjects", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TutorSubjects_Tutor_TutorID",
                        column: x => x.TutorID,
                        principalTable: "Tutor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avialabilities",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    Day = table.Column<string>(type: "text", nullable: false),
                    TutorAvailabilityID = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDepricated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avialabilities", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Avialabilities_Availabilities_TutorAvailabilityID",
                        column: x => x.TutorAvailabilityID,
                        principalTable: "Availabilities",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    Topic = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    AreaOfInterestID = table.Column<string>(type: "text", nullable: true),
                    TutorSubjectsID = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDepricated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Subjects_AreaOfInterests_AreaOfInterestID",
                        column: x => x.AreaOfInterestID,
                        principalTable: "AreaOfInterests",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Subjects_TutorSubjects_TutorSubjectsID",
                        column: x => x.TutorSubjectsID,
                        principalTable: "TutorSubjects",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    SubjectID = table.Column<string>(type: "text", nullable: true),
                    TutorID = table.Column<string>(type: "text", nullable: true),
                    StudentId = table.Column<string>(type: "text", nullable: true),
                    Startime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDepricated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sessions_AspNetUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sessions_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Sessions_Tutor_TutorID",
                        column: x => x.TutorID,
                        principalTable: "Tutor",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "RateStudents",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<int>(type: "integer", nullable: false),
                    SessionID = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDepricated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateStudents", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RateStudents_Sessions_SessionID",
                        column: x => x.SessionID,
                        principalTable: "Sessions",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "RateTutors",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    Rate = table.Column<int>(type: "integer", nullable: false),
                    SessionID = table.Column<string>(type: "text", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDepricated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateTutors", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RateTutors_Sessions_SessionID",
                        column: x => x.SessionID,
                        principalTable: "Sessions",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "StudentComments",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    SessionID = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDepricated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentComments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StudentComments_Sessions_SessionID",
                        column: x => x.SessionID,
                        principalTable: "Sessions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TutorComments",
                columns: table => new
                {
                    ID = table.Column<string>(type: "text", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: false),
                    SessiomID = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDepricated = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorComments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TutorComments_Sessions_SessiomID",
                        column: x => x.SessiomID,
                        principalTable: "Sessions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AreaOfInterests_UserId",
                table: "AreaOfInterests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_TutorID",
                table: "Availabilities",
                column: "TutorID");

            migrationBuilder.CreateIndex(
                name: "IX_Avialabilities_TutorAvailabilityID",
                table: "Avialabilities",
                column: "TutorAvailabilityID");

            migrationBuilder.CreateIndex(
                name: "IX_RateStudents_SessionID",
                table: "RateStudents",
                column: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_RateTutors_SessionID",
                table: "RateTutors",
                column: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_Reminders_UserId",
                table: "Reminders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_StudentId",
                table: "Sessions",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_SubjectID",
                table: "Sessions",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_TutorID",
                table: "Sessions",
                column: "TutorID");

            migrationBuilder.CreateIndex(
                name: "IX_StudentComments_SessionID",
                table: "StudentComments",
                column: "SessionID");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_AreaOfInterestID",
                table: "Subjects",
                column: "AreaOfInterestID");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_TutorSubjectsID",
                table: "Subjects",
                column: "TutorSubjectsID");

            migrationBuilder.CreateIndex(
                name: "IX_Tutor_UserId",
                table: "Tutor",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorComments_SessiomID",
                table: "TutorComments",
                column: "SessiomID");

            migrationBuilder.CreateIndex(
                name: "IX_TutorSubjects_TutorID",
                table: "TutorSubjects",
                column: "TutorID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Avialabilities");

            migrationBuilder.DropTable(
                name: "ImageMeta");

            migrationBuilder.DropTable(
                name: "RateStudents");

            migrationBuilder.DropTable(
                name: "RateTutors");

            migrationBuilder.DropTable(
                name: "Reminders");

            migrationBuilder.DropTable(
                name: "StudentComments");

            migrationBuilder.DropTable(
                name: "TutorComments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Availabilities");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "AreaOfInterests");

            migrationBuilder.DropTable(
                name: "TutorSubjects");

            migrationBuilder.DropTable(
                name: "Tutor");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
