using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Library.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    firstName = table.Column<string>(type: "text", nullable: false),
                    lastName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_authors", x => x.author_id);
                });

            migrationBuilder.CreateTable(
                name: "books",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    isbn = table.Column<string>(type: "text", nullable: true),
                    publisher = table.Column<string>(type: "text", nullable: true),
                    yearOfPublication = table.Column<int>(type: "integer", nullable: true),
                    cover = table.Column<string>(type: "text", nullable: false, defaultValue: "https://justfunfacts.com/wp-content/uploads/2021/03/black.jpg"),
                    description = table.Column<string>(type: "text", nullable: true),
                    pages = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.book_id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    image = table.Column<string>(type: "text", nullable: true, defaultValue: "https://justfunfacts.com/wp-content/uploads/2021/03/black.jpg")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.category_id);
                });

            migrationBuilder.CreateTable(
                name: "spots",
                columns: table => new
                {
                    spot_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    floor = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_spots", x => x.spot_id);
                });

            migrationBuilder.CreateTable(
                name: "statuses",
                columns: table => new
                {
                    status_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuses", x => x.status_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nickname = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    avatar = table.Column<string>(type: "text", nullable: false, defaultValue: "https://justfunfacts.com/wp-content/uploads/2021/03/black.jpg"),
                    is_admin = table.Column<bool>(type: "boolean", nullable: false),
                    notifications = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    authorsauthor_id = table.Column<int>(type: "integer", nullable: false),
                    booksbook_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.authorsauthor_id, x.booksbook_id });
                    table.ForeignKey(
                        name: "FK_AuthorBook_authors_authorsauthor_id",
                        column: x => x.authorsauthor_id,
                        principalTable: "authors",
                        principalColumn: "author_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_books_booksbook_id",
                        column: x => x.booksbook_id,
                        principalTable: "books",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookCategory",
                columns: table => new
                {
                    booksbook_id = table.Column<int>(type: "integer", nullable: false),
                    categoriescategory_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategory", x => new { x.booksbook_id, x.categoriescategory_id });
                    table.ForeignKey(
                        name: "FK_BookCategory_books_booksbook_id",
                        column: x => x.booksbook_id,
                        principalTable: "books",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCategory_categories_categoriescategory_id",
                        column: x => x.categoriescategory_id,
                        principalTable: "categories",
                        principalColumn: "category_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "book_instances",
                columns: table => new
                {
                    book_instance_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    book_id_fk = table.Column<int>(type: "integer", nullable: false),
                    spot_id_fk = table.Column<int>(type: "integer", nullable: false),
                    status_id_fk = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book_instances", x => x.book_instance_id);
                    table.ForeignKey(
                        name: "FK_book_instances_books_book_id_fk",
                        column: x => x.book_id_fk,
                        principalTable: "books",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_book_instances_spots_spot_id_fk",
                        column: x => x.spot_id_fk,
                        principalTable: "spots",
                        principalColumn: "spot_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_book_instances_statuses_status_id_fk",
                        column: x => x.status_id_fk,
                        principalTable: "statuses",
                        principalColumn: "status_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "passwords",
                columns: table => new
                {
                    password_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    salt = table.Column<string>(type: "text", nullable: false),
                    hash = table.Column<string>(type: "text", nullable: false),
                    rounds = table.Column<int>(type: "integer", nullable: false),
                    user_id_fk = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_passwords", x => x.password_id);
                    table.ForeignKey(
                        name: "FK_passwords_users_user_id_fk",
                        column: x => x.user_id_fk,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "borrows",
                columns: table => new
                {
                    borrow_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    book_instance_id_fk = table.Column<int>(type: "integer", nullable: false),
                    user_id_fk = table.Column<int>(type: "integer", nullable: false),
                    borrowTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    returnTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    returnedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_borrows", x => x.borrow_id);
                    table.ForeignKey(
                        name: "FK_borrows_book_instances_book_instance_id_fk",
                        column: x => x.book_instance_id_fk,
                        principalTable: "book_instances",
                        principalColumn: "book_instance_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_borrows_users_user_id_fk",
                        column: x => x.user_id_fk,
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_booksbook_id",
                table: "AuthorBook",
                column: "booksbook_id");

            migrationBuilder.CreateIndex(
                name: "IX_book_instances_book_id_fk",
                table: "book_instances",
                column: "book_id_fk");

            migrationBuilder.CreateIndex(
                name: "IX_book_instances_spot_id_fk",
                table: "book_instances",
                column: "spot_id_fk");

            migrationBuilder.CreateIndex(
                name: "IX_book_instances_status_id_fk",
                table: "book_instances",
                column: "status_id_fk");

            migrationBuilder.CreateIndex(
                name: "IX_BookCategory_categoriescategory_id",
                table: "BookCategory",
                column: "categoriescategory_id");

            migrationBuilder.CreateIndex(
                name: "IX_borrows_book_instance_id_fk",
                table: "borrows",
                column: "book_instance_id_fk");

            migrationBuilder.CreateIndex(
                name: "IX_borrows_user_id_fk",
                table: "borrows",
                column: "user_id_fk");

            migrationBuilder.CreateIndex(
                name: "IX_passwords_user_id_fk",
                table: "passwords",
                column: "user_id_fk",
                unique: true);

            migrationBuilder.Sql(@"

    -- Dodanie danych do tabeli authors
    INSERT INTO authors (""firstName"", ""lastName"")
    VALUES ('Adam', 'Mickiewicz'),
           ('Antoine', 'de Saint-Exupéry'),
           ('Eric-Emmanuel', 'Schmitt');

    -- Dodanie danych do tabeli categories
    INSERT INTO categories (name, image)
    VALUES ('Dramat', 'https://example.com/category1.jpg'),
           ('Fantastyka', 'https://example.com/category2.jpg'),
           ('Powieść', 'https://example.com/category3.jpg');

    -- Dodanie danych do tabeli books
    INSERT INTO books (title, isbn, publisher, ""yearOfPublication"", cover, description, pages)
    VALUES ('Pan Tadeusz', 'ISBN001', 'Wydawca 1', 1834, 'https://example.com/book1.jpg', 'O Panu Tadeuszu', 200),
    ('Mały książe', 'ISBN002', 'Wydawca 2', 1943, 'https://example.com/book2.jpg', 'O małym księciu', 250),
    ('Oskar i pani Róża', 'ISBN003', 'Wydawca 3', 2002, 'https://example.com/book3.jpg', 'O Oskaże i pani Róży', 300);
	
	INSERT INTO ""AuthorBook"" (authorsauthor_id, booksbook_id)
	VALUES
    (1, 1), 
    (2, 2), 
    (3, 3); 

	-- Wstawianie relacji kategorii i książek
	INSERT INTO ""BookCategory"" (booksbook_id, categoriescategory_id)
	VALUES
    (1, 1), 
    (2, 2), 
    (3, 3); 
					   
	-- Dodanie danych do tabeli spots
	INSERT INTO spots (name, floor, description)
	VALUES ('Spot 1', '1', 'Na 1 piętrze'),
		   ('Spot 2', '2', 'Na 2 piętrze'),
		   ('Spot 3', '3', 'Na 3 piętrze');	   
					   
    -- Dodanie danych do tabeli statuses
	INSERT INTO statuses (name)
	VALUES ('Nie dostępna'),
		   ('Dostępna'),
		   ('W krótce');	   

    -- Dodanie danych do tabeli users
    INSERT INTO users (nickname, email, is_admin, notifications)
    VALUES ('Admin', 'admin@example.com', true, false),
           ('User2', 'user2@example.com', false, true),
           ('User3', 'user3@example.com', false, false);

    -- Dodanie danych do tabeli passwords
    INSERT INTO passwords (salt, hash, rounds, user_id_fk)
    VALUES ('Salt1', 'Hashed1', 10, 1),
           ('Salt2', 'Hashed2', 10, 2),
           ('Salt3', 'Hashed3', 10, 3);

    -- Dodanie danych do tabeli book_instances
    INSERT INTO book_instances (book_id_fk, spot_id_fk, status_id_fk)
    VALUES (1, 1, 1),
           (2, 2, 2),
           (3, 3, 3);
					   
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DropTable(
                name: "BookCategory");

            migrationBuilder.DropTable(
                name: "borrows");

            migrationBuilder.DropTable(
                name: "passwords");

            migrationBuilder.DropTable(
                name: "authors");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "book_instances");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "books");

            migrationBuilder.DropTable(
                name: "spots");

            migrationBuilder.DropTable(
                name: "statuses");
        }
    }
}
