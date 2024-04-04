using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTestClientsAndTodos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Password 12345678
            migrationBuilder.InsertData(
                table: "ApplicationUsers",
                columns: new[] { "ApplicationUserId", "Login", "PasswordHash", "CreatedDate" },
                values: new object[,]
                {
                    { "17593112-cd8d-4c96-893b-f53c8cc31cda", "TestClient1", "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv", DateTime.UtcNow },
                    { "2b4945ab-97a7-49c8-098b-08dc5356fbaa", "TestClient2", "$MYHASH$V1$10000$+X4Aw24Ud2+zdOsZVfe7S8tvhB2v4gKHMSrUFhWWVO8yZoSv", DateTime.UtcNow }
                });
            
            migrationBuilder.InsertData(
                table: "ApplicationUserApplicationUserRole",
                columns: new[] { "ApplicationUserId", "ApplicationUserRoleId"},
                values: new object[,]
                {
                    { "2b4945ab-97a7-49c8-098b-08dc5356fbaa", 1 },
                    { "17593112-cd8d-4c96-893b-f53c8cc31cda", 1 }
                });
            
            migrationBuilder.InsertData(
                table: "Todos",
                columns: new[] { "Name", "IsDone", "CreatedDate", "OwnerId"},
                values: new object[,]
                {
                    { "TestClient1 todo 1", true, DateTime.UtcNow.AddHours(-30), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 2", true, DateTime.UtcNow.AddHours(-29), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 3", true, DateTime.UtcNow.AddHours(-28), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 4", true, DateTime.UtcNow.AddHours(-27), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 5", true, DateTime.UtcNow.AddHours(-26), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 6", true, DateTime.UtcNow.AddHours(-25), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 7", true, DateTime.UtcNow.AddHours(-24), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 8", true, DateTime.UtcNow.AddHours(-23), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 9", true, DateTime.UtcNow.AddHours(-22), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 10", true, DateTime.UtcNow.AddHours(-21), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 11", true, DateTime.UtcNow.AddHours(-20), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 12", true, DateTime.UtcNow.AddHours(-19), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 13", true, DateTime.UtcNow.AddHours(-18), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 14", true, DateTime.UtcNow.AddHours(-17), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 15", true, DateTime.UtcNow.AddHours(-16), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 16", true, DateTime.UtcNow.AddHours(-15), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 17", true, DateTime.UtcNow.AddHours(-14), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 18", true, DateTime.UtcNow.AddHours(-13), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 19", false, DateTime.UtcNow.AddHours(-12), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 20", true, DateTime.UtcNow.AddHours(-11), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 21", false, DateTime.UtcNow.AddHours(-10), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 22", true, DateTime.UtcNow.AddHours(-9), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 23", false, DateTime.UtcNow.AddHours(-8), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 24", true, DateTime.UtcNow.AddHours(-7), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 25", false, DateTime.UtcNow.AddHours(-6), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 26", true, DateTime.UtcNow.AddHours(-5), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 27", false, DateTime.UtcNow.AddHours(-4), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 28", false, DateTime.UtcNow.AddHours(-3), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 29", false, DateTime.UtcNow.AddHours(-2), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    { "TestClient1 todo 30", false, DateTime.UtcNow.AddHours(-1), "17593112-cd8d-4c96-893b-f53c8cc31cda",},
                    
                    { "TestClient2 todo 1", true, DateTime.UtcNow.AddHours(-30), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 2", true, DateTime.UtcNow.AddHours(-29), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 3", true, DateTime.UtcNow.AddHours(-28), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 4", true, DateTime.UtcNow.AddHours(-27), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 5", true, DateTime.UtcNow.AddHours(-26), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 6", true, DateTime.UtcNow.AddHours(-25), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 7", true, DateTime.UtcNow.AddHours(-24), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 8", true, DateTime.UtcNow.AddHours(-23), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 9", true, DateTime.UtcNow.AddHours(-22), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 10", true, DateTime.UtcNow.AddHours(-21), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 11", true, DateTime.UtcNow.AddHours(-20), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 12", true, DateTime.UtcNow.AddHours(-19), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 13", true, DateTime.UtcNow.AddHours(-18), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 14", true, DateTime.UtcNow.AddHours(-17), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 15", true, DateTime.UtcNow.AddHours(-16), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 16", true, DateTime.UtcNow.AddHours(-15), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 17", true, DateTime.UtcNow.AddHours(-14), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 18", true, DateTime.UtcNow.AddHours(-13), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 19", false, DateTime.UtcNow.AddHours(-12), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 20", true, DateTime.UtcNow.AddHours(-11), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 21", false, DateTime.UtcNow.AddHours(-10), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 22", true, DateTime.UtcNow.AddHours(-9), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 23", false, DateTime.UtcNow.AddHours(-8), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 24", true, DateTime.UtcNow.AddHours(-7), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 25", false, DateTime.UtcNow.AddHours(-6), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 26", true, DateTime.UtcNow.AddHours(-5), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 27", false, DateTime.UtcNow.AddHours(-4), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 28", false, DateTime.UtcNow.AddHours(-3), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 29", false, DateTime.UtcNow.AddHours(-2), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                    { "TestClient2 todo 30", false, DateTime.UtcNow.AddHours(-1), "2b4945ab-97a7-49c8-098b-08dc5356fbaa",},
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
