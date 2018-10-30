﻿// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using SampleSupport;
using Task.Data;

// Version Mad01

namespace SampleQueries
{
    [Title("LINQ Module")]
    [Prefix("Linq")]
    public class LinqSamples : SampleHarness
    {

        private DataSource dataSource = new DataSource();

        [Category("Restriction Operators")]
        [Title("Where - Task 1")]
        [Description("This sample uses the where clause to find all elements of an array with a value less than 5.")]
        public void Linq1()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var lowNums =
                from num in numbers
                where num < 5
                select num;

            Console.WriteLine("Numbers < 5:");
            foreach (var x in lowNums)
            {
                Console.WriteLine(x);
            }
        }

        [Category("Restriction Operators")]
        [Title("Where - Task 2")]
        [Description("This sample return return all presented in market products")]

        public void Linq2()
        {
            var products =
                from p in dataSource.Products
                where p.UnitsInStock > 0
                select p;

            foreach (var p in products)
            {
                ObjectDumper.Write(p);
            }
        }

        /*
          Выдайте список всех клиентов, чей суммарный оборот (сумма всех заказов) 
          превосходит некоторую величину X. 
          Продемонстрируйте выполнение запроса с различными X 
          (подумайте, можно ли обойтись без копирования запроса несколько раз) 
         */
        [Category("This method output list of customers, which sum of all orders is more than X.")]
        [Title("Where - Task 3")]
        [Description("")]

        public void Linq3()
        {
            int x = 1;
            var customers =
                from t in dataSource.Customers
                where t.Orders.Sum(p => p.Total) > x
                select t;
            foreach (var t in customers)
            {
                ObjectDumper.Write(t);
            }
        }
        /*
        [Category("Restriction Operators")]
        [Title("Where - Task 4")]
        [Description("")]

        public void Linq4()
        {
            var suppliers =
                from t in dataSource.Suppliers
                select new { t,  };


            foreach (IGrouping<string, Phone> g in suppliers)
            {
                Console.WriteLine(g.Key);
                foreach (var t in g)
                    Console.WriteLine(t.Name);
                Console.WriteLine();
            }
        }
       */
        // Найдите всех клиентов, у которых были заказы, превосходящие по сумме величину X 

         [Category("Restriction Operators")]
         [Title("Where - Task 5")]
         [Description("This method output list of customers, which had orders is one sum more than five")]

         public void Linq5()
         {
             int x = 1;
             var customers =
                 from t in dataSource.Customers
                 where t.Orders.Any(p => p.Total > x)
                 select t;

             foreach (var t in customers)
             {
                 ObjectDumper.Write(t);
             }
         }

        /* 
         Выдайте список клиентов с указанием, начиная с какого месяца какого года они стали клиентами 
         (принять за таковые месяц и год самого первого заказа) */
         [Category("Restriction Operators")]
         [Title("Where - Task 6")]
         [Description("This method output list of customers with date of his first order")]

         public void Linq6()
         {
             var customers = dataSource.Customers.Where(u => u.Orders.Any())
                 .Select( u =>  new { Customer = u.CustomerID , OrderDate = u.Orders.OrderBy(p => p.OrderDate).Select(p => p.OrderDate).First()}) ;

             foreach (var u in customers)
             {
                 ObjectDumper.Write($"CustomerId = {u.Customer} " +
                     $"Month = {u.OrderDate.Month} Year = {u.OrderDate.Year}");
             }
         }

        /*
         Укажите всех клиентов, у которых указан
         нецифровой почтовый код или не заполнен регион или в телефоне не указан код оператора 
         (для простоты считаем, что это равнозначно «нет круглых скобочек в начале»).
         */
         [Category("Restriction Operators")]
         [Title("Where - Task 8")]
         [Description("")]

         public void Linq8()
         {
             var customers =
               from t in dataSource.Customers
               where t.Region == string.Empty
                 || t.Phone.First() != '('
                 || t.PostalCode.All(u => u >= '0' && u <= '9')
               select t;
             foreach (var t in customers)
             {
                 ObjectDumper.Write(t);
             }
         }
     }
 }
