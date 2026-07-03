using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeuProjeto.Migrations
{
    /// <inheritdoc />
    public partial class InitalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias", 
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    senha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    foto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pontos = table.Column<int>(type: "int", nullable: false),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jogos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    valor = table.Column<float>(type: "real", nullable: false),
                    capa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    banner = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    categoriaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jogos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jogos_Categorias_categoriaId",
                        column: x => x.categoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carrinhos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carrinhos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carrinhos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Compras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    valor_total = table.Column<float>(type: "real", nullable: false),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Compras_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JogoUsuario",
                columns: table => new
                {
                    JogosId = table.Column<int>(type: "int", nullable: false),
                    UsuariosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JogoUsuario", x => new { x.JogosId, x.UsuariosId });
                    table.ForeignKey(
                        name: "FK_JogoUsuario_Jogos_JogosId",
                        column: x => x.JogosId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JogoUsuario_Usuarios_UsuariosId",
                        column: x => x.UsuariosId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioJogos",
                columns: table => new
                {
                    usuarioId = table.Column<int>(type: "int", nullable: false),
                    jogoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioJogos", x => new { x.usuarioId, x.jogoId });
                    table.ForeignKey(
                        name: "FK_UsuarioJogos_Jogos_jogoId",
                        column: x => x.jogoId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioJogos_Usuarios_usuarioId",
                        column: x => x.usuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarrinhoJogo",
                columns: table => new
                {
                    CarrinhosId = table.Column<int>(type: "int", nullable: false),
                    JogosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoJogo", x => new { x.CarrinhosId, x.JogosId });
                    table.ForeignKey(
                        name: "FK_CarrinhoJogo_Carrinhos_CarrinhosId",
                        column: x => x.CarrinhosId,
                        principalTable: "Carrinhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrinhoJogo_Jogos_JogosId",
                        column: x => x.JogosId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarrinhoJogos",
                columns: table => new
                {
                    carrinhoId = table.Column<int>(type: "int", nullable: false),
                    jogoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoJogos", x => new { x.carrinhoId, x.jogoId });
                    table.ForeignKey(
                        name: "FK_CarrinhoJogos_Carrinhos_carrinhoId",
                        column: x => x.carrinhoId,
                        principalTable: "Carrinhos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrinhoJogos_Jogos_jogoId",
                        column: x => x.jogoId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompraJogo",
                columns: table => new
                {
                    ComprasId = table.Column<int>(type: "int", nullable: false),
                    JogosId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraJogo", x => new { x.ComprasId, x.JogosId });
                    table.ForeignKey(
                        name: "FK_CompraJogo_Compras_ComprasId",
                        column: x => x.ComprasId,
                        principalTable: "Compras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompraJogo_Jogos_JogosId",
                        column: x => x.JogosId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompraJogos",
                columns: table => new
                {
                    compraId = table.Column<int>(type: "int", nullable: false),
                    jogoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompraJogos", x => new { x.compraId, x.jogoId });
                    table.ForeignKey(
                        name: "FK_CompraJogos_Compras_compraId",
                        column: x => x.compraId,
                        principalTable: "Compras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompraJogos_Jogos_jogoId",
                        column: x => x.jogoId,
                        principalTable: "Jogos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    metodo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    criado_em = table.Column<DateTime>(type: "datetime2", nullable: false),
                    compraId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagamentos_Compras_compraId",
                        column: x => x.compraId,
                        principalTable: "Compras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoJogo_JogosId",
                table: "CarrinhoJogo",
                column: "JogosId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoJogos_jogoId",
                table: "CarrinhoJogos",
                column: "jogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Carrinhos_UsuarioId",
                table: "Carrinhos",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompraJogo_JogosId",
                table: "CompraJogo",
                column: "JogosId");

            migrationBuilder.CreateIndex(
                name: "IX_CompraJogos_jogoId",
                table: "CompraJogos",
                column: "jogoId");

            migrationBuilder.CreateIndex(
                name: "IX_Compras_UsuarioId",
                table: "Compras",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Jogos_categoriaId",
                table: "Jogos",
                column: "categoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_JogoUsuario_UsuariosId",
                table: "JogoUsuario",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_compraId",
                table: "Pagamentos",
                column: "compraId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioJogos_jogoId",
                table: "UsuarioJogos",
                column: "jogoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrinhoJogo");

            migrationBuilder.DropTable(
                name: "CarrinhoJogos");

            migrationBuilder.DropTable(
                name: "CompraJogo");

            migrationBuilder.DropTable(
                name: "CompraJogos");

            migrationBuilder.DropTable(
                name: "JogoUsuario");

            migrationBuilder.DropTable(
                name: "Pagamentos");

            migrationBuilder.DropTable(
                name: "UsuarioJogos");

            migrationBuilder.DropTable(
                name: "Carrinhos");

            migrationBuilder.DropTable(
                name: "Compras");

            migrationBuilder.DropTable(
                name: "Jogos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
