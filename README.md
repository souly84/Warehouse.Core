# Warehouse.Core

[![Warehouse.Core package in Warehouse feed in Azure Artifacts](https://souleymen.feeds.visualstudio.com/5e7ba3a8-de58-4498-aed2-a23e91696074/_apis/public/Packaging/Feeds/6754a99f-fc1f-4540-be65-d313fae61071/Packages/2b86139a-c6c0-4de8-890c-5f8541a7d552/Badge)](https://souleymen.visualstudio.com/Warehouse/_packaging?_a=package&feed=6754a99f-fc1f-4540-be65-d313fae61071&package=2b86139a-c6c0-4de8-890c-5f8541a7d552&preferRelease=true)
[![Hits-of-Code](https://hitsofcode.com/github/souly84/Warehouse.Core?branch=main)](https://hitsofcode.com/github/souly84/Warehouse.Core?branch=main/view)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=ncloc)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=coverage)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)

The repository contains domain entities and contracts that help to manage goods in warehouse storage. The root domain entity is [ICompany](https://github.com/souly84/Warehouse.Core/blob/main/src/(Company)/ICompany.cs) provides an access to all business entities. The repository represents Entities project in [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) approach.

## Plugins

Warehouse.Core is currently extended with the particular plugins that implement contracts declared in current repository. Instructions on how to use them are in own repositories linked below.

| Repository | Version |
| ------ | ------ |
| [EbSoft.SDK](https://github.com/souly84/EbSoft.Warehouse.SDK) | [![EbSoft.Warehouse.SDK package in Warehouse feed in Azure Artifacts](https://souleymen.feeds.visualstudio.com/5e7ba3a8-de58-4498-aed2-a23e91696074/_apis/public/Packaging/Feeds/6754a99f-fc1f-4540-be65-d313fae61071/Packages/3e29a369-faf2-402c-b043-6f2deb71a29f/Badge)](https://souleymen.visualstudio.com/Warehouse/_packaging?_a=package&feed=6754a99f-fc1f-4540-be65-d313fae61071&package=3e29a369-faf2-402c-b043-6f2deb71a29f&preferRelease=true) |
| [Oddo.SDK](https://github.com/souly84/Odoo.Warehouse.SDK) | [![Odoo.Warehouse.SDK package in Warehouse feed in Azure Artifacts](https://souleymen.feeds.visualstudio.com/5e7ba3a8-de58-4498-aed2-a23e91696074/_apis/public/Packaging/Feeds/6754a99f-fc1f-4540-be65-d313fae61071/Packages/c19438d5-fdc4-45b8-9c95-c60edf85c208/Badge)](https://souleymen.visualstudio.com/Warehouse/_packaging?_a=package&feed=6754a99f-fc1f-4540-be65-d313fae61071&package=c19438d5-fdc4-45b8-9c95-c60edf85c208&preferRelease=true) |
| [Warehouse.Mobile](https://github.com/souly84/Warehouse.Mobile) | None

## Company

The diagram below shows all the entities that company interface provides access to. At the moment only 2 entities were described in details:

- Warehouse
- Supplier

![Company UML diagram](/docs/Company.uml.svg?raw=true "Classes dependencies diagram")

## Warehouse

The diagram below shows all the entities that warehouse interface provides access to. The main responsibility is to give access to collection of goods in the storage. There are a couple of major features that warehouse employee is processing:

- Order goods preparation for delivery (collecting goods and move then to PutAway storage).
- Goods movement inside the warehouse between the storages.

![Warehouse UML diagram](/docs/warehouse.uml.svg?raw=true "Classes dependencies diagram")

## Supplier

The diagram below shows all the entities that supplier interface provides access to:

- The main responsibility is to give access to collection of receptions that need to be validated by warehouse employee.

![Supplier UML diagram](/docs/Supplier.uml.svg?raw=true "Classes dependencies diagram")

## Status

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=alert_status)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)
[![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=security_rating)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)

## Technical Depth

[![Bugs](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=bugs)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)
[![Code Smells](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=code_smells)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)
[![Duplicated Lines (%)](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=duplicated_lines_density)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)
[![Vulnerabilities](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=vulnerabilities)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)
[![Technical Debt](https://sonarcloud.io/api/project_badges/measure?project=souly84_InventoryOperations&metric=sqale_index)](https://sonarcloud.io/dashboard?id=souly84_InventoryOperations)
