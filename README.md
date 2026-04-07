Chemical Management System — Sprint 2

Author: Hata
Data: 1 Prill 2026
Version: 1.0.0

✨ Përmbledhje Projekti

Ky projekt menaxhon një listë chemical-esh dhe lejon përdoruesin të:

🔍 Kërkojë chemical sipas emrit
➕ Shtojë chemical të ri
⚠️ Trajtojë çdo error pa crash të programit

Arkitektura e projektit: UI → Service → Repository

📂 Struktura e Projektit
MyProject/
│
├── Models/             # Chemical.cs
├── Repositories/       # IRepository.cs, FileRepository.cs
├── Services/           # ChemicalService.cs
├── UI/                 # ConsoleUI.cs
├── Tests/              # xUnit tests + FakeRepository.cs
├── docs/               # sprint-plan.md, sprint-report.md
└── Program.cs          # Lidhja e UI → Service → Repository
🔹 Funksionalitetet Kryesore
1️⃣ Search Chemical
User shkruan emrin e chemical në console
Kontrollohet:
Emër bosh/null → "Ju lutem shkruani input valid"
Chemical nuk ekziston → "Chemical nuk u gjet."
Chemical ekziston → "U gjet: {Name} (ID: {Id})"
2️⃣ Add Chemical
Shton chemical të ri me emër valid
Input bosh → "Emri nuk mund të jetë bosh!"
Sukses → "Chemical u shtua me sukses!"
Lista ruhet automatikisht në data.json
⚠️ Error Handling
Rasti	Shtresa	Mesazh
File mungon	Repository	"File nuk u gjet, po krijoj file të ri..."
Input bosh/null	UI	"Ju lutem shkruani input valid"
Chemical nuk ekziston	Service	"Chemical nuk u gjet."
Exception leximi/ruajtje	Repository	"Gabim gjatë leximit/ruajtjes së file-it."
Exception Search/Add	Service	"Gabim gjatë kërkimit/shtimit."

💡 Programi nuk crashon kurrë.

✅ Unit Tests
Minimum 3 teste (Search + Add)
Raste kufitare: emër bosh, emër nuk ekziston, lista bosh
[Fact] public void Search_ExistingItem_ReturnsItem() { ... }
[Fact] public void Search_NonExisting_ReturnsNull() { ... }
[Fact] public void Add_EmptyName_ReturnsFalse() { ... }
🖥️ Shembuj Output
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
🔄 Workflow i Programit
Program.cs inicializon Repository → Service → UI
UI merr input dhe thërret Service
Service merr/ruan të dhënat nga Repository
Repository lexon/shkruan JSON dhe trajton çdo exception
Unit Tests kontrollojnë logjikën me FakeRepository
🧠 Çka Mësova
Arkitektura UI → Service → Repository
Error handling me try-catch
Krijimi i unit tests për raste normale dhe kufitare
⚡ Si të Ekzekutosh
Sigurohu që ke .NET 6+
Build dhe run:
dotnet build
dotnet run --project MyProject
Run tests:
dotnet test
