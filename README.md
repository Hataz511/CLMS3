Chemical Management System — Sprint 2

Përmbledhje Projekti

Ky projekt është një Chemical Management System që lejon përdoruesin të:

Shikojë listën e chemical-ve.
Kërkojë chemical sipas emrit (Search feature).
Shtojë chemical të ri (Add feature).
Trajtojë çdo error pa crash të programit.

Arkitektura e projektit është UI → Service → Repository, ku:

UI (ConsoleUI.cs): Merret me input nga përdoruesi dhe shfaq output.
Service (ChemicalService.cs): Përmban logjikën e biznesit, si kërkimi dhe shtimi.
Repository (FileRepository.cs): Lexon dhe shkruan të dhënat në file JSON.
Struktura e Projektit
MyProject/
│
├── Models/
│   └── Chemical.cs         # Modeli i Chemical (Id, Name)
│
├── Repositories/
│   ├── IRepository.cs      # Interface e repository
│   └── FileRepository.cs   # Lexim/Shkrim JSON me error handling
│
├── Services/
│   └── ChemicalService.cs  # Logjika: Search + Add + error handling
│
├── UI/
│   └── ConsoleUI.cs        # Meny interactive + input validation
│
├── Tests/
│   ├── ChemicalServiceTests.cs   # xUnit tests
│   └── FakeRepository.cs         # Fake repo për test
│
├── docs/
│   ├── sprint-plan.md
│   └── sprint-report.md
│
└── Program.cs              # Lidhja e UI → Service → Repository
Funksionalitetet Kryesore
1. Kërkim i Chemical (Search) 🔍

Flow:

User shkruan emrin e chemical në console.
ConsoleUI thërret ChemicalService.Search(name).
Service merr të gjithë chemical-et nga repository.
Kontrollohet:
Nëse emri input është bosh/null → kthen null
Nëse nuk ekziston në listë → kthen null
Nëse ekziston → kthen chemical objektin

Mesazhe:

Nëse gjendet: "U gjet: {Name} (ID: {Id})"
Nëse nuk gjendet: "Chemical nuk u gjet."
Nëse input bosh: "Ju lutem shkruani input valid"
2. Shtim i Chemical (Add) ➕

Flow:

User shkruan emrin e chemical në console.
ConsoleUI thërret ChemicalService.Add(name).
Kontrollohet:
Input bosh/null → kthen false, shfaq mesazh "Emri nuk mund të jetë bosh!"
Input valid → shtohet chemical i ri me Id = count + 1
Repository ruan listën e re në data.json

Mesazhe:

Sukses: "Chemical u shtua me sukses!"
Gabim: "Gabim gjatë shtimit."
Error Handling Detajuar ⚠️
Rasti	Shtresa	Si Trajtohet	Mesazh
File mungon	Repository	File krijohet automatikisht	"File nuk u gjet, po krijoj file të ri..."
Input bosh/null	UI	Kontrollohet para thirrjes në Service	"Ju lutem shkruani input valid"
Chemical nuk ekziston	Service	Kontrollohet rezultati i Search	"Chemical nuk u gjet."
Exception gjatë leximit	Repository	Try-catch rreth File.ReadAllText	"Gabim gjatë leximit të file-it."
Exception gjatë ruajtjes	Repository	Try-catch rreth File.WriteAllText	"Gabim gjatë ruajtjes së file-it."
Exception në Search	Service	Try-catch rreth logjikës së kërkimit	"Gabim gjatë kërkimit."
Exception në Add	Service	Try-catch rreth logjikës së shtimit	"Gabim gjatë shtimit."
Input i pavlefshëm në UI	UI	Try-catch rreth Console.ReadLine	"Zgjedhje e pavlefshme!"

Pika kryesore: Programi nuk crashon kurrë.

Unit Tests ✅
Projekt i krijuar me xUnit.
Minimum 3 teste të kryera.
[Fact]
public void Search_ExistingItem_ReturnsItem() { ... }

[Fact]
public void Search_NonExisting_ReturnsNull() { ... }

[Fact]
public void Add_EmptyName_ReturnsFalse() { ... }

FakeRepository: përdoret për të simuluar listën pa ndikuar në file reale.

Raste kufitare që mbulohen:

Emër bosh (Add/ Search)
Emër nuk ekziston (Search)
Lista bosh (Search)
Shembuj Output 📄
--- MENU ---
1. Kerko Chemical
2. Shto Chemical
0. Dil
Zgjedh: 1
Shkruaj emrin: Acid
U gjet: Acid (ID: 1)

Zgjedh: 1
Shkruaj emrin: XYZ
Chemical nuk u gjet.

Zgjedh: 2
Shkruaj emrin: 
Emri nuk mund të jetë bosh!

Zgjedh: 2
Shkruaj emrin: Water
Chemical u shtua me sukses!
Workflow i Programit 🛠️
Program.cs inicializon:
var repository = new FileRepository<Chemical>("data.json");
var service = new ChemicalService(repository);
var ui = new ConsoleUI(service);
ui.Run();
UI merr input dhe thërret Service.
Service thërret Repository për të marrë ose ruajtur të dhënat.
Repository lexon/shkruan JSON dhe trajton çdo exception.
Unit Tests kontrollojnë logjikën e Service me FakeRepository.
Çka Mësova
Implementimi i arkitekturës UI → Service → Repository
Trajtimi i të gjitha error-eve me try-catch
Si të krijoj unit tests që mbulojnë raste normale dhe kufitare
Si të Ekzekutosh Projektin
Sigurohu që ke .NET 6+.
Build dhe run:
dotnet build
dotnet run --project MyProject
Run unit tests:
dotnet test
