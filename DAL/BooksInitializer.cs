using book_store_for_developers.Migrations;
using book_store_for_developers.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using static book_store_for_developers.Models.IdentityModels;

namespace book_store_for_developers.DAL
{
    public class BooksInitializer : MigrateDatabaseToLatestVersion<BooksContext, Configuration>
    {
        public static void SeedBooksData(BooksContext context)
        {
            var categories = new List<Category>
            {
                new Category() {CategoryId=1,CategoryName="Programming", IconFileName="programing.png",CategoryDescyption="Books for programmers"},
                new Category() {CategoryId=2,CategoryName="Office applications", IconFileName="officeApplications.png",CategoryDescyption="Books for office workers"},
                new Category() {CategoryId=3,CategoryName="Database", IconFileName="database.png",CategoryDescyption="Books for database specialists"},
                new Category() {CategoryId=4,CategoryName="Networks and servers", IconFileName="networksAndServers.png",CategoryDescyption="Books for networkers and administrators"},
                new Category() {CategoryId=5,CategoryName="Security", IconFileName="security.png",CategoryDescyption="Books about application security"},
                new Category() {CategoryId=6,CategoryName="Computer Graphics", IconFileName="computerGraphics.png",CategoryDescyption="Books for graphic designers"},
                new Category() {CategoryId=7,CategoryName="Others", IconFileName="Others.jpg",CategoryDescyption="Other books"},
            };

            categories.ForEach(k => context.Categories.AddOrUpdate(k));
            context.SaveChanges();

            var books = new List<Book>
            {
              new Book() {BookId=1,BookAuthor="Prata Stephen",BookTitle="Język C++. Szkoła programowania",CategoryId=1,BookPrice=64,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller =true,ImageFileName="jezykCpp.jpg"},
              new Book() {BookId=2,BookAuthor="Schildt Herbert",BookTitle="Java. Przewodnik dla początkujących",CategoryId=1,BookPrice=72,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="java.jpg"},
              new Book() {BookId=3,BookAuthor="Grolemund Garrett",BookTitle="Język R. Kompletny zestaw narzędzi dla analityków danych",CategoryId=1,BookPrice=59,ReleaseDate=Convert.ToDateTime("2020-08-04"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="jezykR.jpg"},
              new Book() {BookId=4,BookAuthor="Matthes Eric",BookTitle="Python. Instrukcje dla programisty",CategoryId=1,BookPrice=64,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="python.jpg"},
              new Book() {BookId=4,BookAuthor="Haverbeke Marijn",BookTitle="Zrozumieć JavaScript. Wprowadzenie do programowania",CategoryId=1,BookPrice=64,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="javascript.jpg"},
              new Book() {BookId=5,BookAuthor="Wróblewski Piotr",BookTitle="Algorytmy, struktury danych i techniki programowania dla programistów Java",CategoryId=1,BookPrice=89,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="algorytmy.jpg"},
              new Book() {BookId=6,BookAuthor="Farbaniec Dawid",BookTitle="Asembler. Programowanie",CategoryId=1,BookPrice=30,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="asembler.jpg"},
              new Book() {BookId=7,BookAuthor="Walkenbach John",BookTitle="Excel. Programowanie w VBA dla bystrzaków",CategoryId=2,BookPrice=42,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="Excel.jpg"},
              new Book() {BookId=8,BookAuthor="Wrotek Witold",BookTitle="Office 2019 PL. Kurs",CategoryId=2,BookPrice=31,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="Office.jpg"},
              new Book() {BookId=9,BookAuthor="Wrotek Witold",BookTitle="VBA dla Excela 2019 PL. 234 praktyczne przykłady",CategoryId=2,BookPrice=50,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="vba.jpg"},
              new Book() {BookId=10,BookAuthor="Lewis Cindy",BookTitle="Microsoft Project 2019. Krok po kroku",CategoryId=2,BookPrice=48,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="project.jpg"},
              new Book() {BookId=11,BookAuthor="Danowski Bartosz",BookTitle="Facebook. Włącz się do gry",CategoryId=2,BookPrice=12,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="fb.jpg"},
              new Book() {BookId=12,BookAuthor="Lambert Joan",BookTitle="Microsoft Word 2016. Krok po kroku",CategoryId=2,BookPrice=64,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="word.jpg"},
              new Book() {BookId=13,BookAuthor="Szeliga Marcin",BookTitle="SQL. Praktyczny kurs",CategoryId=3,BookPrice=31,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="sql.jpg"},
              new Book() {BookId=14,BookAuthor="Stephenson David",BookTitle="Big data, nauka o danych i AI bez tajemnic. Podejmuj lepsze decyzje i rozwijaj swój biznes!",CategoryId=3,BookPrice=85,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="bigdata.jpg"},
              new Book() {BookId=15,BookAuthor="Chodkowska-Gyurics Agnieszka",BookTitle="Hurtownia danych. Teoria i praktyka",CategoryId=3,BookPrice=58,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="hd.jpg"},
              new Book() {BookId=16,BookAuthor="Elmasri Ramez",BookTitle="Wprowadzenie do systemów baz danych",CategoryId=3,BookPrice=150,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="sysbazdabych.jpg"},
              new Book() {BookId=17,BookAuthor="Il Sohn",BookTitle="Big Data w przemyśle. Jak wykorzystać analizę danych do optymalizacji kosztów procesów?",CategoryId=3,BookPrice=64,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="bigdata2.jpg"},
              new Book() {BookId=18,BookAuthor="Spałek Seweryn",BookTitle="Analiza danych w zarządzaniu projektami",CategoryId=3,BookPrice=38,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="analizadanych.jpg"},
              new Book() {BookId=19,BookAuthor="Zaręba Paweł",BookTitle="Praktyczne projekty sieciowe. Opanuj sieci – w praktyce!",CategoryId=4,BookPrice=50,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="psieciowe.jpg"},
              new Book() {BookId=20,BookAuthor="Bradford Russell",BookTitle="Podstawy sieci komputerowych",CategoryId=4,BookPrice=51,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="sieci.jpg"},
              new Book() {BookId=21,BookAuthor="Thomas Orin",BookTitle="Windows Server 2019 Inside Out",CategoryId=4,BookPrice=97,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="ws.png"},
              new Book() {BookId=22,BookAuthor="Lidermann Krzysztof",BookTitle="Bezpieczeństwo informacyjne. Nowe wyzwania",CategoryId=5,BookPrice=63,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="bi.jpg"},
              new Book() {BookId=23,BookAuthor="Karbowski Marcin",BookTitle="Podstawy kryptografii",CategoryId=5,BookPrice=38,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="pk.jpg"},
              new Book() {BookId=24,BookAuthor="Opracowanie zbiorowe",BookTitle="Hakowanie sztucznej inteligencji",CategoryId=5,BookPrice=58,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="hakowanie.jpg"},
              new Book() {BookId=25,BookAuthor="Faulkner Andrew ",BookTitle="Adobe Photoshop PL. Oficjalny podręcznik. Edycja 2020",CategoryId=6,BookPrice=48,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="photoshop.jpg"},
              new Book() {BookId=26,BookAuthor="Cypryjański Maciej",BookTitle="E-sport. Optymalizacja gracza",CategoryId=7,BookPrice=25,ReleaseDate=Convert.ToDateTime("05-05-2020"),DateAdded=DateTime.Now,Bestseller=true,ImageFileName="esport.jpg"},
            };
            books.ForEach(k => context.Books.AddOrUpdate(k));
            context.SaveChanges();

        }

        public static void SeedUsers(BooksContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            const string name = "admin@gmail.pl";
            const string password = "Haslo1234!";
            const string roleName = "Admin";

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new ApplicationUser { UserName = name, Email = name, UserData = new UserData() };
                var result = userManager.Create(user, password);
            }


            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }


            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }
        }
    }
}
