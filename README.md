# Global.ProductManagement 
É um microserviço que lida com o dominio de Produtos, utilizando uma Web API .NET 8. </br>
O Microserviço <b>Global.ProductManagement</b> realiza o controle das informações referentes aos produtos utilizando o <b>Redis</b> como Cache, o <b>Kafka</b> para o processo de mensageria e o Banco de dados <b>SQL Server</b> para persistência dos dados.

Passo a Passo

1. Execute o <b>docker-compose</b> na raiz deste repositório para que o banco <b>Sql Server</b>, o cache <b>Redis</b> e o broker <b>Kafka</b> sejam criados. 

2. Execute o projeto com o seguinte comando <b>dotnet Global.ProductManagement.Api.dll</b> ou se preferir acesse a solução pelo <b>Visual Studio</b>.

3. Acesse o endpoint da documentação da api em seu navegador https://localhost:7084/swagger

4. Seguem as listas de categorias e marcas para realizar o cadastramento dos produtos:

### Categorias

| ID                                   | NAME       |
|--------------------------------------|------------|
| D0B47DE8-4A55-47D8-BCDD-209A3C6828E4 | Category 3 |
| F91D60C2-2D0C-4F51-B88B-636DAF0DFB7D | Category 8 |
| 30E27B1D-5C77-46D0-8DEE-6EBDB4DA23BA | Category 10|
| 03951DEA-85CA-4964-9462-84FBDCD45E40 | Category 4 |
| 6FE1E3D9-E652-4C58-B912-9209AE32E904 | Category 2 |
| CBEDF365-0B50-4132-BA08-A138E645FEAE | Category 5 |
| A2E4F926-FF90-4C6C-BAF5-AF84175B0D3D | Category 9 |
| 5A861B6F-7BEB-42DB-A963-E41DAEEB7630 | Category 7 |
| B8118600-4003-4CAA-9344-F1CD1F218AA5 | Category 1 |
| B535EF75-ED0E-4F58-BE0C-FC9DD1CF40A0 | Category 6 |

### Marcas

| ID                                   | NAME    |
|--------------------------------------|---------|
| BF314B5B-2E81-439D-AAB2-11BFBD8F0F59 | Brand 9 |
| BE75E2CB-E129-4537-81C3-22A2F92EA7EF | Brand 8 |
| F7DCACC4-BD09-4C96-81CB-263BB4F058C2 | Brand 6 |
| 7E8F2363-E812-4E19-8612-448AE2E109CF | Brand 7 |
| 4B6350D5-5F9C-4925-BA4B-703A14763A81 | Brand 2 |
| 2534AC19-952B-4335-9797-81CA22220F35 | Brand 10|
| 8BBC8343-F18E-4872-9038-8449D205ED1B | Brand 4 |
| 94536EAD-466A-43EF-9146-A12683C50464 | Brand 5 |
| 3849FD79-0666-43FE-B390-CE2EF4CBCBA7 | Brand 1 |
| DEA39053-3104-4E23-8675-FE361FFB733D | Brand 3 |

