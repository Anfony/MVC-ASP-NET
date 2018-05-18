namespace DXMVCWebApplication1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AgregaFK_IdPersona__SIS : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "FK_IdPersona__SIS", c => c.Int(nullable: false));

            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('07874e02-485f-401a-bd32-d0005e5a8558',NULL,0,'AOuNY871QNMVh751qXcRBPQF1l/oS0YnxPXmOlxlRSw0Tq1UVE9I1hyGRGKmKmfvJg==','3b20712e-b37c-4961-9144-3c2ceddafece',NULL,0,0,NULL,0,0,'teuf',2,101,14,11,0,1005,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('0ca87ca8-736e-4241-bd1c-a2470e311316',NULL,0,'AFk2YXtbOWYRrYPNNet3nNlgjekogLXW/5g6LvkX6Nf/WLNsGHSX4cjOcJhfBnjEXQ==','e9f820cc-aa1f-462a-9481-c3ffa3d6bc35',NULL,0,0,NULL,0,0,'AdminUnidadResponsable1',1,100,6,9,0,2,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('0db7c195-7acc-4ace-9e58-b80346bc291d',NULL,0,'AJARV6NfNXuRTsgF0Z6ExNnu3lfceIZ1daqyiDq+votWkKlePNY7Ak7un4dm4DEpjQ==','04cac237-e5d7-46c9-8a5b-57ca6398ca0b',NULL,0,0,NULL,0,0,'R1k4rd01',1,100,6,9,0,4,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('17a916a8-89db-44f2-929d-5d65ff55edc7',NULL,0,'AHG+xDkTiP9bJgMlI9TOZumfXO+O54djWmOB7rz9PnkP2Y0VnoNUZR2v4Aq4iBYinA==','26dafb00-65f9-41d2-92f4-a31ac17f7a3f',NULL,0,0,NULL,0,0,'alberto.valle',0,0,0,0,0,0,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('18c60bbf-ea92-4fa2-9cb0-cab0029d7ad3',NULL,0,'AMTnCbvRg7wuJ3PLmeMZYZPC8aO3rfmK7lhd/NmmJvckY9PNdulUcg58NZpAH4eQ6g==','8e277b71-ad07-4706-9d70-3bfbbe7c4961',NULL,0,0,NULL,0,0,'admin',0,0,0,0,0,0,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('206de551-34d3-4f1c-9733-0b7cc089f759',NULL,0,'AECcgrgmnPr04pqPyBILQylGa3ozfksG/0rUsGMD+AnCdZYMfx+55SXxGtVnkZe9mw==','93358b46-fa24-45f0-8f30-c66f0602822b',NULL,0,0,NULL,0,0,'MANUELA.GOMEZG',25,124,6,11,0,4,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('42c4ba28-0c36-4b00-b529-0512ac8f4ae1',NULL,0,'AEKFYsxuCr6wDokgTOcGilpGm9iIcXUrsGEYKzyPqz9/YBcnIevHaJfYJdjicaNyNQ==','bf9cc5ee-09c9-443c-9b81-cf772ca167a9',NULL,0,0,NULL,0,0,'tres',1,100,6,9,0,2,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('42ef7806-0983-4fe1-9453-9d540cb842b6',NULL,0,'AJR2/GmpJ3z4AMG4ZjMG8kQaBuNPtu12X8omo0wXwCfwUdYFO7GlPPeBar94/LQvog==','0b828773-fb7b-4cc7-9493-bc068f3630c1',NULL,0,0,NULL,0,0,'cuatro',1,100,6,9,0,2,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('563f7ec4-0dfa-40ec-a63f-2d98bc8ac24b',NULL,0,'AFWQZUWRgu+fNcul0uOIQQRO5CS2GNW/GO0J2b6Lgi6/++vEz/3U8rHtYb7i7oFK+Q==','8c841621-0036-4ca0-bced-f5a9d2e7a6ce',NULL,0,0,NULL,0,0,'UNO',1,100,6,9,0,2,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('5779444b-5f32-475a-b301-773c5c0434c4',NULL,0,'AJxtAdZ12U+FF6VRtBQlf/F8xw1hFfNr4T6cOj1kwF20DgTUaaDp7AadQMtRFUXr2w==','7399c9d9-d35e-45ad-9cd4-2c8b2499a9d5',NULL,0,0,NULL,0,0,'Sandy',1,100,6,9,0,2,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('5dc8fb45-ac92-4fbc-87eb-c736f4bd5ef0',NULL,0,'AAfnDWNCkooG5cjCQEGSL7iaiQSCkeOJMFNN2LagYyOaVcfp9Ny5hu+vsS7Sez6+bw==','8abd2bd6-f62a-4182-996f-542775cdb558',NULL,0,0,NULL,0,0,'abc',1,100,6,9,0,2,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('64493a81-0aad-4540-9ba0-7ffef2a6a0e5',NULL,0,'ABu5zRNGhzgAKWjpJXQXyoOvL/2XZ8kNtSfmMFOdZbdm3r7AyGbq0nkCp1leD1XWhQ==','36925e73-21c4-4121-9b63-b48ac7f89c03',NULL,0,0,NULL,0,0,'testing',0,0,0,0,0,0,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('73b29038-4394-4d34-aad8-f756e0151092',NULL,0,'ABpwhLn+T7N4wXPOIClJrDwQ2xFlZv9mZFsGTgeqYkjdhSrpsfX5xLnatc8tJjMilg==','546d6a9b-7840-47ca-9206-94f03bde9c1f',NULL,0,0,NULL,0,0,'ab',1,100,6,9,0,2,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('79df05b8-245a-4b64-96fd-67248b73feea',NULL,0,'ALr/2XvJ+a5qCfSok8/ucmhChYKhFIdKIrMFvdAEtzQMcfOR5Fq6Ik9/39JENxA8gQ==','f41e9eea-119a-44c0-b180-0d311a8a3613',NULL,0,0,NULL,0,0,'prueb',1,100,6,9,0,2,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('7fed15bb-ee3b-41b1-a121-863b08f90743',NULL,0,'ALJNdzyO4qcQHAMtEium8nia9ahbE97ICgygQzivJ9iiTobosH86GTqg4+lHxRY/Bg==','4e270410-fa92-4fb8-93c6-f3e3b77ed4df',NULL,0,0,NULL,0,0,'doss',1,100,6,9,0,2,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('8a30b85b-f7cf-41a6-bed7-b1e25b16e7a2',NULL,0,'ADIh3L7NiJfuIt4NvYoIzTrn7RPiy0dzKmGoPhcg35L+KQ2EDhnsqJx21+SGgZ2mgA==','612c841f-4100-488e-ab26-f93fac292b82',NULL,0,0,NULL,0,0,'angie',1,100,6,9,0,4,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('8d49212f-5cb7-407a-bfec-6d712e1c07e4',NULL,0,'APmOJCHXyHe3IVT6ou5PyDsqVf+ifJDIW/ptQwOthfWITvkkDUgQwWfp77iGR5SM/Q==','f41bdcf8-d7e6-4082-833e-265da55df8a9',NULL,0,0,NULL,0,0,'jaime.robles',25,124,6,9,0,2,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('902ac88b-3949-4145-9597-d99892441fae',NULL,0,'AGbvjk4epDoZBYGoa8HKMEg61E8JtsGNzhaEjjq3YQgceG7OkEDUmEiv9UkLnUzkHA==','67d22bb3-7c62-48df-8b34-a3ac1b95acd1',NULL,0,0,NULL,0,0,'Usuario1',1,100,6,9,0,2,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('981c146b-29aa-4f26-b6ed-5cca2ca4033d',NULL,0,'AIqdvGoE4JM1pMJitsQxQsdXBqavpmGVys8USRMhiivYh6qTZ5bkPWu424RRg45pbw==','671ce147-665e-4469-b822-4360ea32e6b3',NULL,0,0,NULL,0,0,'AIESVAUG',1,100,6,9,4,2,1263)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('a5dc6bb1-7103-41b4-aa2f-160549d95215',NULL,0,'AEqW3Vv7mftRzrLmS/l0uDQRaUIK2jTl6qc4YrOIBtcBjA7P1n8TtLzqjhRuoRvWZw==','46947e56-f6bd-4baa-aa60-a40572a96409',NULL,0,0,NULL,0,0,'UNOS',1,100,6,9,0,2,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('a9a89d52-5648-4654-881d-ea075f99750d',NULL,0,'AOQ6WDnQPSe/wPt77LWItDnkp9Q6vDeesvP8aeACHBGFuc0Ubshk+CHIsm8ny6Gr2A==','7a411b98-63ef-4a4c-9cda-5e5645c9ac8e',NULL,0,0,NULL,0,0,'abruz',1,100,6,9,0,4,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('b95f76c3-dba6-4624-b9f4-9bf570bbda4c',NULL,0,'AL3ctSHzZJPmeY90haJszgpXSy+sm1N5hYAF1ooIoOXssKkr4xdMM/6Lz0lSKp5wiw==','07861fd8-0cea-404f-89f5-0380b227beec',NULL,0,0,NULL,0,0,'AdminInstanciaEjecutora2',2,101,6,9,0,2,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('b9c9d61f-4e0e-405f-a01c-00f76c41dec7',NULL,0,'ANUVCqkSJdJr8v0MH4kSFRuM6Y9QYUe5PbJo90LmJdcGkOYKWizbmC/bmvbeVW0epw==','3e5b2878-0b08-4fe0-adce-99ba31f3ae51',NULL,0,0,NULL,0,0,'probando',1,100,6,9,0,2,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('c93a24e4-a4bd-4310-aa41-08d4d5c3781e',NULL,0,'ADT7SO1feFFVFCmVtEuXa7Sb12C9zM30GkqPYGR33k7N+6r/cfHrXpB7+189ascSgA==','24688363-1e9e-4045-b9cf-c82ea90c60a8',NULL,0,0,NULL,0,0,'a',1,100,6,9,0,2,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('d35ecef1-7f17-44d1-878f-8e164b5178ff',NULL,0,'ALToqAhJsMVRYYGEYUSuRqG+gdRPjnbn5/u55AQN6Ka8QwcE+nHhouOzkQ/c0cb/dQ==','74270dce-1097-4dca-9fa8-653af0eeb3b8',NULL,0,0,NULL,0,0,'AdminInstanciaEjecutora1',1,100,6,9,0,2,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('d9e42337-590c-4973-9c1b-b0de1e27f3cb',NULL,0,'AFyEJQN+0Gj9LRrx0Wl18vduhED1xt0mq34EVODWh3TOqNitj6tN7Jf0A34rTpWyHw==','5fd4dde9-0cf2-412a-9fe6-a86d3bfa7ba4',NULL,0,0,NULL,0,0,'daciaisabel',7,343,6,12,0,1009,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('dcaf1232-ae69-496c-8c2d-a078127465ef',NULL,0,'AN8upkEZOLjg3pitfCSBYJ0gbQN//y7JNrdSLd7RRQ4XJ511vnHQqBs6ZfNVEjxC9w==','a0597ab4-3e1b-4559-b0dd-d0273a609d67',NULL,0,0,NULL,0,0,'Admin1',1,100,6,9,0,2,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('ecb50380-c63a-4eab-930a-9e6f12f1bc04',NULL,0,'AF3eKzk6WLBbSIXiKsgKEGsZC0WpPEv2X4tKu6z+YvggkxfNJOfjbDkC2N4f4YX1jw==','2a0f290f-26dd-4bcc-bd66-74ba44bb3111',NULL,0,0,NULL,0,0,'asd',1,350,6,11,4,4,0)");
            Sql("INSERT INTO [dbo].[AspNetUsers] VALUES('fd9bce77-34e2-427b-a211-295186372534',NULL,0,'ALjXIQRTZadBKPW7EfLwEVVF/7aQe4WuL133cD5ZkLesstKqaHGDu9HdapbvmG4p4w==','a5b3eee3-10f0-42e4-acaa-c3db4635f46f',NULL,0,0,NULL,0,0,'AdminUnidadResponsable2',2,101,6,9,0,2,0)");
            Sql("INSERT INTO [dbo].[AspNetRoles] VALUES('ce9d47ce-a2bf-4c17-a6af-e26e6e5ee5a3','Admin')");
            Sql("INSERT INTO [dbo].[AspNetRoles] VALUES('b706ce6b-57a3-4daa-9d96-d3fe410f5be3','Administrador de Instancia Ejecutora')");
            Sql("INSERT INTO [dbo].[AspNetRoles] VALUES('27e083c2-85f4-4910-abb1-662a0422f5bc','Administrador de Instancia Ejecutora de Inocuidad')");
            Sql("INSERT INTO [dbo].[AspNetRoles] VALUES('ef284f40-5e38-4afb-a71a-748c1787c744','Administrador de Instancia Ejecutora de Movilizacion')");
            Sql("INSERT INTO [dbo].[AspNetRoles] VALUES('f84fd72c-9ac4-4db8-91c9-6c97bd338a89','Administrador de Instancia Ejecutora de Salud Animal')");
            Sql("INSERT INTO [dbo].[AspNetRoles] VALUES('0d873cd3-2a84-4946-8783-c1c736948dbf','Administrador de Instancia Ejecutora de Sanidad Acuicola y Pesquera')");
            Sql("INSERT INTO [dbo].[AspNetRoles] VALUES('7f4ba6a4-045b-42a6-b535-79f8d965122e','Administrador de Instancia Ejecutora de Sanidad Vegetal')");
            Sql("INSERT INTO [dbo].[AspNetRoles] VALUES('cd596075-8919-44c5-8c2b-ce27f8789f70','Administrador de Unidad Responsable')");
            Sql("INSERT INTO [dbo].[AspNetRoles] VALUES('6be6c591-d2b4-46e4-a38a-59071add29f1','Administrador de Unidad Responsable de Inocuidad')");
            Sql("INSERT INTO [dbo].[AspNetRoles] VALUES('eb9045b6-17c1-4f36-89af-dfd58c2e7115','Administrador de Unidad Responsable de Movilizacion')");
            Sql("INSERT INTO [dbo].[AspNetRoles] VALUES('ff6c83ee-0fec-4ba9-9be8-8165b26fb84e','Administrador de Unidad Responsable de Salud Animal')");
            Sql("INSERT INTO [dbo].[AspNetRoles] VALUES('1c539716-6721-404a-9c15-9a6551fb86d0','Administrador de Unidad Responsable de Sanidad Acuicola y Pesquera')");
            Sql("INSERT INTO [dbo].[AspNetRoles] VALUES('2af24c0a-3aca-4d92-a122-f43058a6b10e','Administrador de Unidad Responsable de Sanidad Vegetal')");
            Sql("INSERT INTO [dbo].[AspNetRoles] VALUES('5c0fb16c-13cb-4802-8c21-627511b6f7f9','User')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('902ac88b-3949-4145-9597-d99892441fae','5c0fb16c-13cb-4802-8c21-627511b6f7f9')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('981c146b-29aa-4f26-b6ed-5cca2ca4033d','7f4ba6a4-045b-42a6-b535-79f8d965122e')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('07874e02-485f-401a-bd32-d0005e5a8558','b706ce6b-57a3-4daa-9d96-d3fe410f5be3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('8d49212f-5cb7-407a-bfec-6d712e1c07e4','b706ce6b-57a3-4daa-9d96-d3fe410f5be3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('b95f76c3-dba6-4624-b9f4-9bf570bbda4c','b706ce6b-57a3-4daa-9d96-d3fe410f5be3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('d35ecef1-7f17-44d1-878f-8e164b5178ff','b706ce6b-57a3-4daa-9d96-d3fe410f5be3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('ecb50380-c63a-4eab-930a-9e6f12f1bc04','b706ce6b-57a3-4daa-9d96-d3fe410f5be3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('0ca87ca8-736e-4241-bd1c-a2470e311316','cd596075-8919-44c5-8c2b-ce27f8789f70')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('fd9bce77-34e2-427b-a211-295186372534','cd596075-8919-44c5-8c2b-ce27f8789f70')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('0db7c195-7acc-4ace-9e58-b80346bc291d','ce9d47ce-a2bf-4c17-a6af-e26e6e5ee5a3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('206de551-34d3-4f1c-9733-0b7cc089f759','ce9d47ce-a2bf-4c17-a6af-e26e6e5ee5a3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('42c4ba28-0c36-4b00-b529-0512ac8f4ae1','ce9d47ce-a2bf-4c17-a6af-e26e6e5ee5a3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('42ef7806-0983-4fe1-9453-9d540cb842b6','ce9d47ce-a2bf-4c17-a6af-e26e6e5ee5a3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('563f7ec4-0dfa-40ec-a63f-2d98bc8ac24b','ce9d47ce-a2bf-4c17-a6af-e26e6e5ee5a3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('5779444b-5f32-475a-b301-773c5c0434c4','ce9d47ce-a2bf-4c17-a6af-e26e6e5ee5a3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('5dc8fb45-ac92-4fbc-87eb-c736f4bd5ef0','ce9d47ce-a2bf-4c17-a6af-e26e6e5ee5a3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('73b29038-4394-4d34-aad8-f756e0151092','ce9d47ce-a2bf-4c17-a6af-e26e6e5ee5a3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('79df05b8-245a-4b64-96fd-67248b73feea','ce9d47ce-a2bf-4c17-a6af-e26e6e5ee5a3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('7fed15bb-ee3b-41b1-a121-863b08f90743','ce9d47ce-a2bf-4c17-a6af-e26e6e5ee5a3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('8a30b85b-f7cf-41a6-bed7-b1e25b16e7a2','ce9d47ce-a2bf-4c17-a6af-e26e6e5ee5a3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('a5dc6bb1-7103-41b4-aa2f-160549d95215','ce9d47ce-a2bf-4c17-a6af-e26e6e5ee5a3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('a9a89d52-5648-4654-881d-ea075f99750d','ce9d47ce-a2bf-4c17-a6af-e26e6e5ee5a3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('b9c9d61f-4e0e-405f-a01c-00f76c41dec7','ce9d47ce-a2bf-4c17-a6af-e26e6e5ee5a3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('c93a24e4-a4bd-4310-aa41-08d4d5c3781e','ce9d47ce-a2bf-4c17-a6af-e26e6e5ee5a3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('d9e42337-590c-4973-9c1b-b0de1e27f3cb','ce9d47ce-a2bf-4c17-a6af-e26e6e5ee5a3')");
            Sql("INSERT INTO [dbo].[AspNetUserRoles] VALUES('dcaf1232-ae69-496c-8c2d-a078127465ef','ce9d47ce-a2bf-4c17-a6af-e26e6e5ee5a3')");
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "FK_IdPersona__SIS");
        }
    }
}
