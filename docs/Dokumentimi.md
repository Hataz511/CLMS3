Chemical Laboratory Management System (CLMS) është një sistem i dizajnuar për menaxhimin e:

Inventarit të kimikateve
Proceseve të aprovimit
Sigurisë laboratorike
Gjurmueshmërisë së eksperimenteve
Auditimit të veprimeve

Qëllimi kryesor është:

reduktimi i gabimeve manuale
rritja e sigurisë
përmirësimi i kontrollit dhe transparencës
USHTRIMI 1 – STRUKTURA E PROJEKTIT
✔ Qëllimi

Organizimi i kodit sipas një arkitekture të qartë dhe të mirëmbajtshme.

CLMS/
│
├── Models/                # Domain Entities (Pure Business Objects)
│   ├── User.cs
│   ├── Role.cs
│   ├── Chemical.cs
│   ├── Request.cs
│   ├── Approval.cs
│   ├── Experiment.cs
│   ├── Equipment.cs
│   ├── WasteRecord.cs
│   ├── Incident.cs
│   └── AuditLog.cs
│
├── Services/              # Business Logic Layer
│   ├── Interfaces/
│   │   ├── IUserService.cs
│   │   ├── IChemicalService.cs
│   │   └── IRequestService.cs
│   │
│   ├── Implementations/
│   │   ├── UserService.cs
│   │   ├── ChemicalService.cs
│   │   ├── RequestService.cs
│   │   ├── ExperimentService.cs
│   │   ├── SafetyService.cs
│   │   └── AuditService.cs
│
├── Data/                  # Data Access Layer
│   ├── Interfaces/
│   │   └── IRepository.cs
│   │
│   ├── Repositories/
│   │   └── FileRepository.cs
│   │
│   └── Utils/
│       └── CsvSerializer.cs
│
├── UI/                    # Presentation Layer
│   └── ConsoleUI.cs
│
├── docs/
│   ├── architecture.md
│   ├── class-diagram.md
│   └── decisions.md       # EXTRA (HIGH SCORE)
│
├── Program.cs
├── README.md
└── .gitignore

Përshkrimi i shtresave
🔹 Models (Domain Layer)
Përmban entitetet kryesore të sistemit
Nuk përmban logjikë biznesi komplekse

Shembuj:

User
Chemical
Request
🔹 Services (Business Logic Layer)
Implementon rregullat e biznesit
Validon operacionet

Shembuj:

ChemicalService
RequestService
🔹 Data (Data Access Layer)
Menaxhon ruajtjen e të dhënave
Implementon Repository Pattern
🔹 UI (Presentation Layer)
Ndërveprim me përdoruesin
Delegon operacionet tek Services
🧠 Vendime arkitekturore
Përdorimi i Layered Architecture
Ndarje e qartë e përgjegjësive
Rritje e maintainability dhe scalability

USHTRIMI 2 – UML CLASS DIAGRAM
✔ Qëllimi

Vizualizimi i strukturës së sistemit dhe relacioneve midis klasave.

✔ Çfarë përfshin diagrami
Klasa (User, Chemical, Request, etc.)
Atribute private
Metoda publike
Relacione (association, dependency)
✔ Elemente kryesore
Domain entities
Interfaces (IRepository, IChemicalService)
Service classes
Repository classes
🎯 Përfitime
Kuptueshmëri më e mirë e sistemit
Dokumentim profesional
Ndihmon në validimin e dizajnit

USHTRIMI 3 – REPOSITORY PATTERN
✔ Qëllimi

Ndarja e logjikës së aksesit në të dhëna nga logjika e biznesit.

✔ Interface
public interface IRepository<T>
{
    IEnumerable<T> GetAll();
    T GetById(int id);
    void Add(T entity);
    void Save();
}
✔ Implementimi
public class FileRepository<T> : IRepository<T>
✔ Storage
CSV file-based storage
🧠 Përfitime
Loose coupling
Testability
Extendability (DB në të ardhmen)

USHTRIMI 4 – ARCHITECTURE DOCUMENTATION
✔ Shtresat
UI Layer
Service Layer
Data Layer
Domain Layer
✔ Data Flow

User → UI → Service → Repository → File

✔ Design Patterns
Repository Pattern
Dependency Injection (manual)
Layered Architecture
✔ Trade-offs
Vendim	Përfitim	Kufizim
CSV Storage	Thjeshtësi	Jo scalable
Layered Architecture	Clean	Overhead

SOLID PRINCIPLES IMPLEMENTATION
✔ SRP (Single Responsibility)
ChemicalService → vetëm business logic
FileRepository → vetëm data
NotificationService → vetëm alerts
✔ OCP (Open/Closed)
IRepository lejon zgjerim pa ndryshim të kodit ekzistues
✔ LSP (Liskov)
FileRepository dhe DatabaseRepository janë të zëvendësueshme
✔ ISP (Interface Segregation)
Interfaces të vogla dhe specifike
IChemicalService
INotificationService
✔ DIP (Dependency Inversion)
Services varen nga interface
Injection bëhet në ConsoleUI
7️⃣ 🔄 DEPENDENCY INJECTION
✔ Implementimi
var repo = new FileRepository<Chemical>("chemicals.csv");
var notification = new NotificationService();
var service = new ChemicalService(repo, notification);
✔ Vendndodhja
ConsoleUI (Composition Root)
🎯 Përfitime
Loose coupling
Flexibility
Testability

UML DIAGRAMS (SUMMARY)

Projekti përfshin:

✔ Class Diagram
✔ Sequence Diagram
✔ Activity Diagram
✔ Component Diagram
✔ Deployment Diagram
9️⃣ ⚡ NON-FUNCTIONAL REQUIREMENTS
✔ Performance
Operacione të thjeshta në memory
✔ Security
Audit log
Role-based access
✔ Scalability
Mundësi migrimi në DB