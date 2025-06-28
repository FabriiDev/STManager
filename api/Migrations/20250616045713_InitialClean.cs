using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class InitialClean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cliente",
                columns: table => new
                {
                    id_cliente = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellido = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    direccion = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    celular = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    otros = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cliente", x => x.id_cliente);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "configuracion",
                columns: table => new
                {
                    id_configuracion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre_taller = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    direccion_taller = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    valor_diagnostico = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    url_logo = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefono_taller = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    eslogan = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    texto_pie = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    texto_legal = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_configuracion", x => x.id_configuracion);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "equipo_marca",
                columns: table => new
                {
                    id_marca_equipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    marca = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipo_marca", x => x.id_marca_equipo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "equipo_tipo",
                columns: table => new
                {
                    id_tipo_equipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tipo = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipo_tipo", x => x.id_tipo_equipo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "impresion_material",
                columns: table => new
                {
                    id_material = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    tipo_material = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    color = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_impresion_material", x => x.id_material);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "metodo_pago",
                columns: table => new
                {
                    id_metodo_pago = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    metodo = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_metodo_pago", x => x.id_metodo_pago);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "producto_imagen",
                columns: table => new
                {
                    id_imagen = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    url_imagen = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producto_imagen", x => x.id_imagen);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "producto_marca",
                columns: table => new
                {
                    id_marca_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    marca_producto = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producto_marca", x => x.id_marca_producto);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "producto_tipo",
                columns: table => new
                {
                    id_tipo_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tipo_producto = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producto_tipo", x => x.id_tipo_producto);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "servicio",
                columns: table => new
                {
                    id_servicio = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    precio = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_servicio", x => x.id_servicio);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "usuario",
                columns: table => new
                {
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    email = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nick = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    rol = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id_usuario);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "equipo_modelo",
                columns: table => new
                {
                    id_modelo_equipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    modelo = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_marca_equipo = table.Column<int>(type: "int", nullable: false),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipo_modelo", x => x.id_modelo_equipo);
                    table.ForeignKey(
                        name: "FK_equipo_modelo_equipo_marca_id_marca_equipo",
                        column: x => x.id_marca_equipo,
                        principalTable: "equipo_marca",
                        principalColumn: "id_marca_equipo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "producto_modelo",
                columns: table => new
                {
                    id_modelo_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    modelo_producto = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_marca_producto = table.Column<int>(type: "int", nullable: false),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producto_modelo", x => x.id_modelo_producto);
                    table.ForeignKey(
                        name: "FK_producto_modelo_producto_marca_id_marca_producto",
                        column: x => x.id_marca_producto,
                        principalTable: "producto_marca",
                        principalColumn: "id_marca_producto",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "caja",
                columns: table => new
                {
                    id_caja = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fecha_apertura = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    fecha_cierre = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    total_ventas = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    total_ordenes = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    observaciones = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_usuario_apertura = table.Column<int>(type: "int", nullable: false),
                    id_usuario_cierre = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caja", x => x.id_caja);
                    table.ForeignKey(
                        name: "FK_caja_usuario_id_usuario_apertura",
                        column: x => x.id_usuario_apertura,
                        principalTable: "usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_caja_usuario_id_usuario_cierre",
                        column: x => x.id_usuario_cierre,
                        principalTable: "usuario",
                        principalColumn: "id_usuario");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "equipo",
                columns: table => new
                {
                    id_equipo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    numero_serie = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_cliente = table.Column<int>(type: "int", nullable: false),
                    id_tipo_equipo = table.Column<int>(type: "int", nullable: false),
                    id_modelo_equipo = table.Column<int>(type: "int", nullable: false),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IdMarcaEquipo = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipo", x => x.id_equipo);
                    table.ForeignKey(
                        name: "FK_equipo_cliente_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "cliente",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_equipo_equipo_marca_IdMarcaEquipo",
                        column: x => x.IdMarcaEquipo,
                        principalTable: "equipo_marca",
                        principalColumn: "id_marca_equipo");
                    table.ForeignKey(
                        name: "FK_equipo_equipo_modelo_id_modelo_equipo",
                        column: x => x.id_modelo_equipo,
                        principalTable: "equipo_modelo",
                        principalColumn: "id_modelo_equipo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_equipo_equipo_tipo_id_tipo_equipo",
                        column: x => x.id_tipo_equipo,
                        principalTable: "equipo_tipo",
                        principalColumn: "id_tipo_equipo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "producto",
                columns: table => new
                {
                    id_producto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    descripcion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    precio = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    stock = table.Column<int>(type: "int", nullable: false),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    id_tipo_producto = table.Column<int>(type: "int", nullable: false),
                    id_imagen_producto = table.Column<int>(type: "int", nullable: true),
                    id_marca_producto = table.Column<int>(type: "int", nullable: false),
                    id_modelo_producto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_producto", x => x.id_producto);
                    table.ForeignKey(
                        name: "FK_producto_producto_imagen_id_imagen_producto",
                        column: x => x.id_imagen_producto,
                        principalTable: "producto_imagen",
                        principalColumn: "id_imagen");
                    table.ForeignKey(
                        name: "FK_producto_producto_marca_id_marca_producto",
                        column: x => x.id_marca_producto,
                        principalTable: "producto_marca",
                        principalColumn: "id_marca_producto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_producto_producto_modelo_id_modelo_producto",
                        column: x => x.id_modelo_producto,
                        principalTable: "producto_modelo",
                        principalColumn: "id_modelo_producto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_producto_producto_tipo_id_tipo_producto",
                        column: x => x.id_tipo_producto,
                        principalTable: "producto_tipo",
                        principalColumn: "id_tipo_producto",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "gasto",
                columns: table => new
                {
                    id_gasto = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_caja = table.Column<int>(type: "int", nullable: false),
                    id_metodo_pago = table.Column<int>(type: "int", nullable: false),
                    descripcion = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    monto = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    fecha_gasto = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    estado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_gasto", x => x.id_gasto);
                    table.ForeignKey(
                        name: "FK_gasto_caja_id_caja",
                        column: x => x.id_caja,
                        principalTable: "caja",
                        principalColumn: "id_caja",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gasto_metodo_pago_id_metodo_pago",
                        column: x => x.id_metodo_pago,
                        principalTable: "metodo_pago",
                        principalColumn: "id_metodo_pago",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_gasto_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "venta",
                columns: table => new
                {
                    id_venta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    fecha_venta = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false),
                    id_caja = table.Column<int>(type: "int", nullable: false),
                    estado = table.Column<bool>(type: "tinyint(50)", maxLength: 50, nullable: false),
                    id_metodo_pago = table.Column<int>(type: "int", nullable: false),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_venta", x => x.id_venta);
                    table.ForeignKey(
                        name: "FK_venta_caja_id_caja",
                        column: x => x.id_caja,
                        principalTable: "caja",
                        principalColumn: "id_caja",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_venta_metodo_pago_id_metodo_pago",
                        column: x => x.id_metodo_pago,
                        principalTable: "metodo_pago",
                        principalColumn: "id_metodo_pago",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_venta_usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "orden",
                columns: table => new
                {
                    nro_orden = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_equipo = table.Column<int>(type: "int", nullable: false),
                    id_cliente = table.Column<int>(type: "int", nullable: false),
                    id_usuario_creador = table.Column<int>(type: "int", nullable: false),
                    id_usuario_entrega = table.Column<int>(type: "int", nullable: true),
                    id_usuario_asignado = table.Column<int>(type: "int", nullable: false),
                    falla = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    detalle_tecnico = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_ingreso = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    fecha_entrega = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    fecha_presupuesto = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    cargador = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    extras = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    diagnostico = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    presupuesto = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    pago_diagnostico = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    estado = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    entregada = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orden", x => x.nro_orden);
                    table.ForeignKey(
                        name: "FK_orden_cliente_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "cliente",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orden_equipo_id_equipo",
                        column: x => x.id_equipo,
                        principalTable: "equipo",
                        principalColumn: "id_equipo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orden_usuario_id_usuario_asignado",
                        column: x => x.id_usuario_asignado,
                        principalTable: "usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orden_usuario_id_usuario_creador",
                        column: x => x.id_usuario_creador,
                        principalTable: "usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_orden_usuario_id_usuario_entrega",
                        column: x => x.id_usuario_entrega,
                        principalTable: "usuario",
                        principalColumn: "id_usuario");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "detalle_venta_producto",
                columns: table => new
                {
                    id_detalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_venta = table.Column<int>(type: "int", nullable: false),
                    id_producto = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    precio_unitario = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_venta_producto", x => x.id_detalle);
                    table.ForeignKey(
                        name: "FK_detalle_venta_producto_producto_id_producto",
                        column: x => x.id_producto,
                        principalTable: "producto",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalle_venta_producto_venta_id_venta",
                        column: x => x.id_venta,
                        principalTable: "venta",
                        principalColumn: "id_venta",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "impresion_3d",
                columns: table => new
                {
                    id_impresion_3d = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_cliente = table.Column<int>(type: "int", nullable: false),
                    id_venta = table.Column<int>(type: "int", nullable: true),
                    fecha_ingreso = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    fecha_estimada_entrega = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    fecha_entrega = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    descripcion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    total = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    estado = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    entregada = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    activo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_impresion_3d", x => x.id_impresion_3d);
                    table.ForeignKey(
                        name: "FK_impresion_3d_cliente_id_cliente",
                        column: x => x.id_cliente,
                        principalTable: "cliente",
                        principalColumn: "id_cliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_impresion_3d_venta_id_venta",
                        column: x => x.id_venta,
                        principalTable: "venta",
                        principalColumn: "id_venta");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "detalle_orden_producto",
                columns: table => new
                {
                    id_detalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nro_orden = table.Column<int>(type: "int", nullable: false),
                    id_producto = table.Column<int>(type: "int", nullable: false),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    precio_unitario = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_orden_producto", x => x.id_detalle);
                    table.ForeignKey(
                        name: "FK_detalle_orden_producto_orden_nro_orden",
                        column: x => x.nro_orden,
                        principalTable: "orden",
                        principalColumn: "nro_orden",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalle_orden_producto_producto_id_producto",
                        column: x => x.id_producto,
                        principalTable: "producto",
                        principalColumn: "id_producto",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "detalle_orden_servicio",
                columns: table => new
                {
                    id_detalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_orden = table.Column<int>(type: "int", nullable: false),
                    id_servicio = table.Column<int>(type: "int", nullable: false),
                    precio = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalle_orden_servicio", x => x.id_detalle);
                    table.ForeignKey(
                        name: "FK_detalle_orden_servicio_orden_id_orden",
                        column: x => x.id_orden,
                        principalTable: "orden",
                        principalColumn: "nro_orden",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalle_orden_servicio_servicio_id_servicio",
                        column: x => x.id_servicio,
                        principalTable: "servicio",
                        principalColumn: "id_servicio",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "impresion_item",
                columns: table => new
                {
                    id_impresion_item = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    id_impresion_3d = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descripcion = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cantidad = table.Column<int>(type: "int", nullable: false),
                    id_material = table.Column<int>(type: "int", nullable: false),
                    precio_unitario = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_impresion_item", x => x.id_impresion_item);
                    table.ForeignKey(
                        name: "FK_impresion_item_impresion_3d_id_impresion_3d",
                        column: x => x.id_impresion_3d,
                        principalTable: "impresion_3d",
                        principalColumn: "id_impresion_3d",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_impresion_item_impresion_material_id_material",
                        column: x => x.id_material,
                        principalTable: "impresion_material",
                        principalColumn: "id_material",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_caja_id_usuario_apertura",
                table: "caja",
                column: "id_usuario_apertura");

            migrationBuilder.CreateIndex(
                name: "IX_caja_id_usuario_cierre",
                table: "caja",
                column: "id_usuario_cierre");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_orden_producto_id_producto",
                table: "detalle_orden_producto",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_orden_producto_nro_orden",
                table: "detalle_orden_producto",
                column: "nro_orden");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_orden_servicio_id_orden",
                table: "detalle_orden_servicio",
                column: "id_orden");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_orden_servicio_id_servicio",
                table: "detalle_orden_servicio",
                column: "id_servicio");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_venta_producto_id_producto",
                table: "detalle_venta_producto",
                column: "id_producto");

            migrationBuilder.CreateIndex(
                name: "IX_detalle_venta_producto_id_venta",
                table: "detalle_venta_producto",
                column: "id_venta");

            migrationBuilder.CreateIndex(
                name: "IX_equipo_id_cliente",
                table: "equipo",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_equipo_id_modelo_equipo",
                table: "equipo",
                column: "id_modelo_equipo");

            migrationBuilder.CreateIndex(
                name: "IX_equipo_id_tipo_equipo",
                table: "equipo",
                column: "id_tipo_equipo");

            migrationBuilder.CreateIndex(
                name: "IX_equipo_IdMarcaEquipo",
                table: "equipo",
                column: "IdMarcaEquipo");

            migrationBuilder.CreateIndex(
                name: "IX_equipo_modelo_id_marca_equipo",
                table: "equipo_modelo",
                column: "id_marca_equipo");

            migrationBuilder.CreateIndex(
                name: "IX_gasto_id_caja",
                table: "gasto",
                column: "id_caja");

            migrationBuilder.CreateIndex(
                name: "IX_gasto_id_metodo_pago",
                table: "gasto",
                column: "id_metodo_pago");

            migrationBuilder.CreateIndex(
                name: "IX_gasto_id_usuario",
                table: "gasto",
                column: "id_usuario");

            migrationBuilder.CreateIndex(
                name: "IX_impresion_3d_id_cliente",
                table: "impresion_3d",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_impresion_3d_id_venta",
                table: "impresion_3d",
                column: "id_venta");

            migrationBuilder.CreateIndex(
                name: "IX_impresion_item_id_impresion_3d",
                table: "impresion_item",
                column: "id_impresion_3d");

            migrationBuilder.CreateIndex(
                name: "IX_impresion_item_id_material",
                table: "impresion_item",
                column: "id_material");

            migrationBuilder.CreateIndex(
                name: "IX_orden_id_cliente",
                table: "orden",
                column: "id_cliente");

            migrationBuilder.CreateIndex(
                name: "IX_orden_id_equipo",
                table: "orden",
                column: "id_equipo");

            migrationBuilder.CreateIndex(
                name: "IX_orden_id_usuario_asignado",
                table: "orden",
                column: "id_usuario_asignado");

            migrationBuilder.CreateIndex(
                name: "IX_orden_id_usuario_creador",
                table: "orden",
                column: "id_usuario_creador");

            migrationBuilder.CreateIndex(
                name: "IX_orden_id_usuario_entrega",
                table: "orden",
                column: "id_usuario_entrega");

            migrationBuilder.CreateIndex(
                name: "IX_producto_id_imagen_producto",
                table: "producto",
                column: "id_imagen_producto");

            migrationBuilder.CreateIndex(
                name: "IX_producto_id_marca_producto",
                table: "producto",
                column: "id_marca_producto");

            migrationBuilder.CreateIndex(
                name: "IX_producto_id_modelo_producto",
                table: "producto",
                column: "id_modelo_producto");

            migrationBuilder.CreateIndex(
                name: "IX_producto_id_tipo_producto",
                table: "producto",
                column: "id_tipo_producto");

            migrationBuilder.CreateIndex(
                name: "IX_producto_modelo_id_marca_producto",
                table: "producto_modelo",
                column: "id_marca_producto");

            migrationBuilder.CreateIndex(
                name: "IX_venta_id_caja",
                table: "venta",
                column: "id_caja");

            migrationBuilder.CreateIndex(
                name: "IX_venta_id_metodo_pago",
                table: "venta",
                column: "id_metodo_pago");

            migrationBuilder.CreateIndex(
                name: "IX_venta_id_usuario",
                table: "venta",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "configuracion");

            migrationBuilder.DropTable(
                name: "detalle_orden_producto");

            migrationBuilder.DropTable(
                name: "detalle_orden_servicio");

            migrationBuilder.DropTable(
                name: "detalle_venta_producto");

            migrationBuilder.DropTable(
                name: "gasto");

            migrationBuilder.DropTable(
                name: "impresion_item");

            migrationBuilder.DropTable(
                name: "orden");

            migrationBuilder.DropTable(
                name: "servicio");

            migrationBuilder.DropTable(
                name: "producto");

            migrationBuilder.DropTable(
                name: "impresion_3d");

            migrationBuilder.DropTable(
                name: "impresion_material");

            migrationBuilder.DropTable(
                name: "equipo");

            migrationBuilder.DropTable(
                name: "producto_imagen");

            migrationBuilder.DropTable(
                name: "producto_modelo");

            migrationBuilder.DropTable(
                name: "producto_tipo");

            migrationBuilder.DropTable(
                name: "venta");

            migrationBuilder.DropTable(
                name: "cliente");

            migrationBuilder.DropTable(
                name: "equipo_modelo");

            migrationBuilder.DropTable(
                name: "equipo_tipo");

            migrationBuilder.DropTable(
                name: "producto_marca");

            migrationBuilder.DropTable(
                name: "caja");

            migrationBuilder.DropTable(
                name: "metodo_pago");

            migrationBuilder.DropTable(
                name: "equipo_marca");

            migrationBuilder.DropTable(
                name: "usuario");
        }
    }
}
