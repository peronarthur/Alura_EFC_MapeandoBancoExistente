using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Alura.Filmes.App.Migrations
{
    public partial class View : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"CREATE VIEW [dbo].[top5_most_starred_actors]
                        AS
	                        select top 5 a.actor_id, a.first_name, a.last_name, count(fa.film_id) as total
	                        from actor a
		                        inner join film_actor fa on fa.actor_id = a.actor_id
	                        group by a.actor_id, a.first_name, a.last_name
	                        order by total desc";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = @"DROP VIEW [dbo].[top5_most_starred_actors]";

            migrationBuilder.Sql(sql);
        }
    }
}
