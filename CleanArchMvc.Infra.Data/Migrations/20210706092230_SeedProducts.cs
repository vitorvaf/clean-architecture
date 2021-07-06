using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchMvc.Infra.Data.Migrations
{
    public partial class SeedProducts : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO public.\"Products\" (\"Name\", \"Description\", \"Price\", \"Stock\", \"Image\", \"CategoryId\") " +
                   "VALUES('Caderno espiral', 'Caderno espiral 100 folhas', 7.45, 50, 'caderno.jpg', 1);");            

        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Products");
        }
    }
}
