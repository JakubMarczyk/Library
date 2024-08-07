using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Library.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    author_id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    book_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    isbn = table.Column<string>(type: "text", nullable: true),
                    publisher = table.Column<string>(type: "text", nullable: true),
                    yearOfPublication = table.Column<int>(type: "integer", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    pages = table.Column<int>(type: "integer", nullable: true),
                    cover = table.Column<string>(type: "text", nullable: true, defaultValue: "https://justfunfacts.com/wp-content/uploads/2021/03/black.jpg")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_books", x => x.book_id);
                });

            migrationBuilder.CreateTable(
                name: "bookshelfs",
                columns: table => new
                {
                    bookshelf_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    floor = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookshelfs", x => x.bookshelf_id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    category_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    image = table.Column<string>(type: "text", nullable: true, defaultValue: "https://justfunfacts.com/wp-content/uploads/2021/03/black.jpg")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.category_id);
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
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    nickname = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: false),
                    is_admin = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    authorsauthor_id = table.Column<Guid>(type: "uuid", nullable: false),
                    booksbook_id = table.Column<Guid>(type: "uuid", nullable: false)
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
                    booksbook_id = table.Column<Guid>(type: "uuid", nullable: false),
                    categoriescategory_id = table.Column<Guid>(type: "uuid", nullable: false)
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
                    book_instance_id = table.Column<Guid>(type: "uuid", nullable: false),
                    book_id_fk = table.Column<Guid>(type: "uuid", nullable: false),
                    bookshelf_id_fk = table.Column<Guid>(type: "uuid", nullable: false),
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
                        name: "FK_book_instances_bookshelfs_bookshelf_id_fk",
                        column: x => x.bookshelf_id_fk,
                        principalTable: "bookshelfs",
                        principalColumn: "bookshelf_id",
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
                    password_id = table.Column<Guid>(type: "uuid", nullable: false),
                    salt = table.Column<string>(type: "text", nullable: false),
                    hash = table.Column<string>(type: "text", nullable: false),
                    user_id_fk = table.Column<Guid>(type: "uuid", nullable: false)
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
                    borrow_id = table.Column<Guid>(type: "uuid", nullable: false),
                    book_instance_id_fk = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id_fk = table.Column<Guid>(type: "uuid", nullable: false),
                    borrowTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    returnTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    returnedTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    extended = table.Column<bool>(type: "boolean", nullable: false)
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
                name: "IX_book_instances_bookshelf_id_fk",
                table: "book_instances",
                column: "bookshelf_id_fk");

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
    INSERT INTO authors (author_id, ""firstName"", ""lastName"")
    VALUES ('88bbb1a1-0fb7-4f72-8d15-f0d32021f79b', 'Adam', 'Mickiewicz'),
           ('77c01b59-cda7-4fc8-ae33-f5ca6d11e0d6', 'Antoine', 'de Saint-Exupéry'),
           ('11c2dd97-1be1-407c-8069-c48c8e42885f', 'Eric-Emmanuel', 'Schmitt');

    -- Dodanie danych do tabeli categories
    INSERT INTO categories (category_id, name)
    VALUES ('11e1b02c-968e-48ed-8616-c7578df743fe', 'Dramat'),
           ('129a44d2-1531-4ab5-a79a-3cbe278d2794', 'Fantastyka'),
           ('42d0cb99-82cf-4804-8e1b-2d30bf315fa7', 'Powieść');

    -- Dodanie danych do tabeli books
    INSERT INTO books (book_id, title, isbn, publisher, ""yearOfPublication"", cover, description, pages)
    VALUES ('955397fb-3eeb-42b0-a359-c19854bdb7e0', 'Pan Tadeusz', 'ISBN001', 'Wydawca 1', 1834, 'https://example.com/book1.jpg', 'O Panu Tadeuszu', 200),
    ('a590dc3d-fb4d-4f6f-8d32-01cb12238bad', 'Mały książe', 'ISBN002', 'Wydawca 2', 1943, 'https://example.com/book2.jpg', 'O małym księciu', 250),
    ('67391b81-4e7b-468f-8bce-8930ad6ba691', 'Oskar i pani Róża', 'ISBN003', 'Wydawca 3', 2002, 'https://example.com/book3.jpg', 'O Oskaże i pani Róży', 300);

	-- Dodanie danych do tabeli bookshelfs
	INSERT INTO bookshelfs (bookshelf_id, name, floor)
	VALUES ('1bb5fe7b-61a2-459d-8748-2eefafe8ee3c', 'Biblioteczka 1', '1'),
		   ('506c8c8b-7bef-41d1-8d36-0ef6823a47ea', 'Biblioteczka 2', '2'),
		   ('771dc6b1-6d55-49a1-b637-2dd0c121a30c', 'Biblioteczka 3', '3');	   
					   
    -- Dodanie danych do tabeli statuses
	INSERT INTO statuses (name)
	VALUES ('Nie dostępna'),
		   ('Dostępna'),
		   ('Zagubiona');	   

    -- Dodanie danych do tabeli users
    INSERT INTO users (user_id, nickname, email, is_admin)
    VALUES ('35f60a35-e18c-41ab-8f28-f242b7d121fe', 'Admin', 'admin@example.com', true),
           ('79e5d3b4-f59f-4241-ba61-b08f5ce995ed', 'User1', 'user1@example.com', false),
           ('d938218f-63d6-4d06-88f1-5424d81703c5', 'User2', 'user2@example.com', false);

	INSERT INTO ""AuthorBook"" (authorsauthor_id, booksbook_id)
	VALUES
    ('88bbb1a1-0fb7-4f72-8d15-f0d32021f79b', '955397fb-3eeb-42b0-a359-c19854bdb7e0'), 
    ('77c01b59-cda7-4fc8-ae33-f5ca6d11e0d6', 'a590dc3d-fb4d-4f6f-8d32-01cb12238bad'), 
    ('11c2dd97-1be1-407c-8069-c48c8e42885f', '67391b81-4e7b-468f-8bce-8930ad6ba691'); 

	-- Wstawianie relacji kategorii i książek
	INSERT INTO ""BookCategory"" (categoriescategory_id, booksbook_id)
	VALUES
    ('11e1b02c-968e-48ed-8616-c7578df743fe', '955397fb-3eeb-42b0-a359-c19854bdb7e0'), 
    ('129a44d2-1531-4ab5-a79a-3cbe278d2794', 'a590dc3d-fb4d-4f6f-8d32-01cb12238bad'), 
    ('42d0cb99-82cf-4804-8e1b-2d30bf315fa7', '67391b81-4e7b-468f-8bce-8930ad6ba691'); 
					      
    -- Dodanie danych do tabeli passwords
    INSERT INTO passwords (password_id, salt, hash, user_id_fk)
    VALUES ('adb0d91e-0dc3-4b2a-8603-3a6465b66678', 'j7O9+1Wr+vhwfr+gMcaxCA==', '5FHpR6PSsSGYquhID8Z81ta4tmugm+jAgeTBbsGknYE=', '35f60a35-e18c-41ab-8f28-f242b7d121fe'),
           ('8767852e-4143-41c2-ac32-3d1ac67962dc', 'NKc+C8aae10/u13HbNS7sw==', '3QeRmvHdicASyMtiyofekKAw+BwvfPMSYJFSBTTlOUA=', '79e5d3b4-f59f-4241-ba61-b08f5ce995ed'),
           ('45a90380-3b2d-4865-9ca2-f309433b97c4', 'KheuKX6Yef24RujWm7s3bQ==', 'OL3apSBRymBoRJdZYGAjYx46HWTHpPp/DTo4G10RWcE=', 'd938218f-63d6-4d06-88f1-5424d81703c5');

    -- Dodanie danych do tabeli book_instances
    INSERT INTO book_instances (book_instance_id, book_id_fk, bookshelf_id_fk, status_id_fk)
    VALUES ('ccbe9b3a-0168-4937-891e-17a934305f75', '955397fb-3eeb-42b0-a359-c19854bdb7e0', '1bb5fe7b-61a2-459d-8748-2eefafe8ee3c', 1),
           ('0404c094-22c2-4c12-83a1-28faf6e0a379', 'a590dc3d-fb4d-4f6f-8d32-01cb12238bad', '506c8c8b-7bef-41d1-8d36-0ef6823a47ea', 2),
           ('89127a5b-3626-4da8-9ca8-149a8798b125', '67391b81-4e7b-468f-8bce-8930ad6ba691', '771dc6b1-6d55-49a1-b637-2dd0c121a30c', 3);
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
                name: "bookshelfs");

            migrationBuilder.DropTable(
                name: "statuses");
        }
    }
}
