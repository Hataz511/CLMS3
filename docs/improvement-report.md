
# improvement-report.md

# Chemical Laboratory Management System (CLMS)

---

# 1️⃣ Executive Summary

Ky dokument paraqet një analizë kritike dhe përmirësim të thelluar të projektit Chemical Laboratory Management System (CLMS).

Qëllimi i këtij sprinti nuk ishte shtimi i funksionaliteteve të reja, por:

* Identifikimi i dobësive arkitekturore
* Përmirësimi i strukturës së kodit
* Rritja e reliability dhe robustness
* Përmirësimi i dokumentimit teknik
* Demonstrimi i mendimit kritik inxhinierik

Janë realizuar 3 përmirësime thelbësore:

* Structural / Code Improvement
* Reliability & Validation Improvement
* Documentation & Architectural Transparency Improvement

---

# 2️⃣ Improvement 1 – Structural Refactor & Architectural Hardening

## 📌 Problemi Fillestar

Në versionin fillestar:

* `FileRepository<T>` përmbante logjikë të përzier:

  * parsing CSV
  * file handling
  * error handling
* Service layer kishte validim minimal
* `Program.cs` kryente manual wiring pa strukturë të qartë
* Nuk kishte abstraction për file handling
* Repository ishte tightly coupled me CSV

👉 Kjo krijonte:

* Tight coupling
* Vështirësi në testim
* Vështirësi në zgjerim (p.sh. kalim në DB)

---

## 🔧 Çfarë U Ndryshua

### 1.1 Ndarja e File Handling

```csharp
public interface IFileStorage
{
    bool FileExists(string path);
    IEnumerable<string> ReadAllLines(string path);
    void WriteAllLines(string path, IEnumerable<string> lines);
}
```

```csharp
public class CsvFileStorage : IFileStorage
{
    public bool FileExists(string path)
        => File.Exists(path);

    public IEnumerable<string> ReadAllLines(string path)
        => File.ReadAllLines(path);

    public void WriteAllLines(string path, IEnumerable<string> lines)
        => File.WriteAllLines(path, lines);
}
```

---

### 1.2 Repository sipas SRP

`FileRepository<T>` tani:

* Nuk merret me File IO
* Nuk merret me validation
* Nuk merret me logging

Merret vetëm me:

* Mapping CSV ↔ Model
* CRUD operacione

---

### 1.3 Dependency Injection

```csharp
IFileStorage storage = new CsvFileStorage();
IRepository<Chemical> repo = new FileRepository<Chemical>(storage);
IChemicalService service = new ChemicalService(repo);
IUserInterface ui = new ConsoleUI(service);

ui.Run();
```

---

## 🔄 Before vs After (Concrete Comparison)

**Before:**

* Repository menaxhonte file IO, parsing dhe error handling
* Nuk kishte ndarje të qartë të përgjegjësive

**After:**

* FileStorage → vetëm file IO
* Repository → vetëm data mapping dhe CRUD
* Service → validation dhe business logic

👉 Ky ndryshim ul kompleksitetin dhe rrit modularitetin.
## System Architecture Flow

UI → Service → Repository → FileStorage → CSV
---

## 🧠 Pse Versioni i Ri Është Më i Mirë

* ✔ Respekton SRP dhe DIP
* ✔ Eliminon tight coupling
* ✔ Rrit testueshmërinë
* ✔ Lehtëson migrimin drejt database

---

# 3️⃣ Improvement 2 – Reliability & Validation Hardening

## 📌 Problemi Fillestar

* CSV që mungon → aplikacioni dështonte
* ID jo-ekzistues → null reference
* Nuk kishte validim për inpute
* Nuk kishte error handling të strukturuar

👉 Sistemi ishte i brishtë (fragile)

---

## 🔧 Çfarë U Ndryshua

### 2.1 Trajtim i file që mungon

```csharp
if (!storage.FileExists(_filePath))
{
    storage.WriteAllLines(_filePath, new List<string>());
}
```

---

### 2.2 Validim në Service Layer

```csharp
public void AddChemical(Chemical chemical)
{
    if (string.IsNullOrWhiteSpace(chemical.Name))
        throw new ArgumentException("Chemical name cannot be empty.");

    if (chemical.Price <= 0)
        throw new ArgumentException("Price must be greater than zero.");

    if (chemical.Quantity < 0)
        throw new ArgumentException("Quantity cannot be negative.");

    _repository.Add(chemical);
    _repository.Save();
}
```

---

### 2.3 Trajtim i ID që nuk ekziston

```csharp
public Chemical GetById(int id)
{
    var chemical = _repository.GetById(id);

    if (chemical == null)
        throw new KeyNotFoundException($"Chemical with ID {id} not found.");

    return chemical;
}
```

---

### 2.4 Try-Catch në UI

```csharp
try
{
    service.DeleteChemical(id);
    Console.WriteLine("Deleted successfully.");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
```

---

## 🧪 Unit Testing (Basic but Critical)

```csharp
[Test]
public void AddChemical_ShouldThrow_WhenNameIsEmpty()
{
    var service = new ChemicalService(mockRepo);

    Assert.Throws<ArgumentException>(() =>
        service.AddChemical(new Chemical { Name = "" }));
}
```

```csharp
[Test]
public void GetById_ShouldThrow_WhenChemicalDoesNotExist()
{
    Assert.Throws<KeyNotFoundException>(() =>
        service.GetById(999));
}
```

👉 Siguron që validimi dhe error handling funksionojnë siç pritet.

---

## 🧠 Pse Versioni i Ri Është Më i Mirë

* ✔ Nuk crash-on më
* ✔ Parandalon input të pavlefshëm
* ✔ Jep feedback të qartë
* ✔ Rrit robustness dhe stabilitetin
### 🎯 Real Impact Scenario

Before:
Nëse file CSV mungonte, aplikacioni dështonte menjëherë.

After:
Aplikacioni krijon automatikisht file-in dhe vazhdon funksionimin pa ndërprerje.

👉 Ky ndryshim eliminon një pikë kritike dështimi dhe rrit stabilitetin e sistemit.
---

# 4️⃣ Improvement 3 – Documentation & Engineering Transparency

## 📌 Problemi Fillestar

* Dokumentim kryesisht përshkrues
* Pa analizë të vendimeve
* Pa identifikim të kufizimeve

---

## 🔧 Çfarë U Ndryshua

### 3.1 architecture.md

* Layer diagram
* Dependency flow
* Arsyetim i arkitekturës

---

### 3.2 Design Decisions

Shembull:

CSV u përdor për thjeshtësi akademike, ndërsa në production do të zëvendësohej me database për scalability dhe concurrency.

---

### 3.3 Known Limitations

* Mungesë concurrency control
* Mungesë logging
* Test coverage i kufizuar
* Mungesë authentication

---

### 3.4 Setup Instructions

1. Klono repository
2. Hap projektin në Visual Studio
3. Run nga `Program.cs`
4. CSV krijohet automatikisht nëse mungon

---

## 🧠 Pse Është Më i Mirë

* ✔ Rrit transparencën
* ✔ Lehtëson onboarding
* ✔ Tregon mendim inxhinierik real

---

# 5️⃣ Reflection – Mendim Kritik

Ky sprint tregoi se:

* Kod që funksionon ≠ kod i mirë
* Strukturë funksionale ≠ strukturë e qëndrueshme

👉 **Gabim konkret:**
Validimi fillimisht ishte në UI, duke shkaktuar shpërndarje të logjikës.
Pas refactor-it u centralizua në Service layer, duke përmirësuar konsistencën dhe mirëmbajtjen.

👉 **Çfarë mësova:**

* Abstraction rrit fleksibilitetin
* Separation of concerns rrit maintainability
* Defensive programming parandalon gabime
* Refactoring është proces i vazhdueshëm

---

# 6️⃣ Çfarë Mbetet Ende e Dobët

* Test coverage i kufizuar
* Nuk ka logging framework (Serilog / NLog)
* Nuk ka concurrency control
* Nuk ka data versioning
* Nuk ka authentication të avancuar

👉 Këto janë hapat e ardhshëm drejt production-grade.

---

# 7️⃣ Final Conclusion

Ky sprint nuk ishte për shtim features, por për:

* Ristrukturim arkitekturor
* Rritje të reliability
* Përmirësim të dokumentimit

Projekti tani:

* Është modular
* Është i testueshëm
* Është robust
* Është i dokumentuar
* Është teknikisht i arsyetuar

👉 Ky rezultat demonstron jo vetëm implementim, por kuptim të thellë të inxhinierisë së softuerit.

---
