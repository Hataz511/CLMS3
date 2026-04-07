# 🧪 CLMS – Architecture Documentation
Chemical Laboratory Management System

---

# 1. 📌 SYSTEM OVERVIEW

CLMS (Chemical Laboratory Management System) është një platformë digjitale e dizajnuar për të menaxhuar në mënyrë të sigurt dhe efikase:

- Inventarin e kimikateve
- Proceset e aprovimit të substancave
- Gjurmueshmërinë e eksperimenteve
- Sigurinë laboratorike
- Auditimin dhe përputhshmërinë

Sistemi synon të reduktojë:
- gabimet manuale
- rrezikun nga substancat e rrezikshme
- mungesën e kontrollit dhe auditimit

---

# 2. 🧱 ARCHITECTURAL STYLE

## 2.1 Layered Architecture (N-Tier)

CLMS përdor një arkitekturë të shtresuar (Layered Architecture), e përbërë nga:

1. Presentation Layer (UI)
2. Application / Service Layer
3. Data Access Layer
4. Domain Layer (Models)

### ✔ Pse kjo arkitekturë?

- Separation of Concerns
- Maintainability
- Scalability
- Testability

---

# 3. 🧩 SYSTEM LAYERS (DETAILED)

---

## 3.1 🎨 Presentation Layer (UI)

### Përgjegjësitë:
- Ndërveprimi me përdoruesin
- Marrja e inputeve
- Shfaqja e rezultateve

### Karakteristika:
- Nuk përmban logjikë biznesi
- Delegon çdo operacion tek Services
- Validim bazik (optional)

### Komponentë:
- ConsoleUI.cs

---

## 3.2 ⚙️ Service Layer (Business Logic Layer)

### Përgjegjësitë:
- Implementimi i rregullave të biznesit
- Koordinimi i proceseve
- Validimi i operacioneve kritike

### Shembuj logjike:
- Hazard access restriction
- Multi-level approval workflow
- Stock validation
- Expiry checks

### Komponentë:
- ChemicalService
- RequestService
- ExperimentService
- SafetyService
- AuditService

---

## 3.3 💾 Data Access Layer

### Përgjegjësitë:
- Ruajtja dhe leximi i të dhënave
- Abstraksioni i burimit të të dhënave

### Pattern:
- Repository Pattern

### Komponentë:
- IRepository<T>
- FileRepository<T>

### Storage:
- CSV files (file-based persistence)

---

## 3.4 🧬 Domain Layer (Models)

### Përgjegjësitë:
- Përfaqësimi i entiteteve të sistemit
- Encapsulation i të dhënave

### Komponentë:
- User
- Chemical
- Request
- Experiment
- AuditLog
- Equipment

### Karakteristika:
- Nuk kanë varësi nga shtresa të tjera
- Janë “pure business objects”

---

# 4. 🔄 DATA FLOW

1. User → UI (input)
2. UI → Service Layer
3. Service Layer → Repository
4. Repository → File System
5. Response → UI

Dependency Injection është implementuar në ConsoleUI (Composition Root), 
ku krijohen dhe lidhen instancat e Repository dhe Services.

Program.cs mbahet minimal për të respektuar separation of concerns.
---

# 5. 🧠 DESIGN PATTERNS USED

---

## 5.1 Repository Pattern

### Qëllimi:
Ndërmjetëson midis domain dhe data source

### Përfitime:
- Loose coupling
- Testability
- Replaceable data source

---

## 5.2 Dependency Injection (Manual)

### Qëllimi:
Injektimi i varësive nga jashtë

### Përfitime:
- Fleksibilitet
- Testim më i lehtë
- Decoupling

---

## 5.3 Layered Pattern

### Qëllimi:
Organizim logjik i sistemit

---

# 6. 🧪 SOLID PRINCIPLES IMPLEMENTATION

---

## 6.1 Single Responsibility Principle (SRP)

Çdo klasë ka një përgjegjësi të vetme:
- Service → logjikë biznesi
- Repository → data persistence
- UI → ndërveprim

---

## 6.2 Open/Closed Principle (OCP)

Sistemi mund të zgjerohet duke shtuar:
- DatabaseRepository
pa ndryshuar kod ekzistues

---

## 6.3 Liskov Substitution Principle (LSP)

Çdo IRepository mund të zëvendësohet pa prishur sistemin

---

## 6.4 Interface Segregation Principle (ISP)

Interfaces janë të ndara sipas funksionit:
- IChemicalService
- IRequestService

---

## 6.5 Dependency Inversion Principle (DIP)

Services varen nga abstractions (IRepository)

## SOLID Implementation Mapping

- SRP: Implemented in separation between Services, Repository, and Notification
- OCP: Achieved via IRepository abstraction
- LSP: All repository implementations follow same contract
- ISP: Services use small, specific interfaces
- DIP: Services depend on abstractions injected via constructors
---

# 7. 🔐 SECURITY ARCHITECTURE

---

## 7.1 Authentication
- Username / Password
- 2FA (conceptual)

## 7.2 Authorization
- Role-Based Access Control (RBAC)

## 7.3 Data Protection
- Audit logs (immutable)
- Input validation

---

# 8. ⚡ NON-FUNCTIONAL REQUIREMENTS IMPLEMENTATION

---

## Performance
- Minimal I/O operations
- Efficient in-memory collections

## Scalability
- Easily replace CSV with DB
- Layered structure supports growth

## Availability
- File-based backup possible

## Security
- Encapsulation
- Audit tracking

---

# 9. 🗄 DATA STORAGE STRATEGY

---

## Current:
- CSV files

## Future:
- PostgreSQL
- Cloud storage

---

# 10. 🔍 ERROR HANDLING STRATEGY

- Try-Catch blocks
- Logging errors
- Fail-safe behavior

---

# 11. 📊 LOGGING & AUDIT

---

## Audit Log:
- Immutable
- Timestamped
- Linked to user

---

# 12. 🔄 TRANSACTION MANAGEMENT

- Simulated atomic operations
- Save() ensures consistency

---

# 13. 🧩 EXTENSIBILITY

Sistemi mund të zgjerohet me:

- REST API
- Web UI
- AI module
- IoT integration

---

# 14. 🚀 FUTURE IMPROVEMENTS

- Microservices architecture
- Docker deployment
- Cloud hosting
- Machine learning analytics

---

# 15. ⚖️ TRADE-OFFS

| Decision | Benefit | Limitation |
|--------|--------|-----------|
| CSV Storage | Simple | Not scalable |
| Layered Architecture | Clean | Slight overhead |

---

# 16. 📌 CONCLUSION

Arkitektura e CLMS është:

✔ Modular  
✔ Scalable  
✔ Maintainable  
✔ Secure  

Dhe ndjek praktikat më të mira të industrisë për sisteme enterprise.