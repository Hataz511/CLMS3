# CLMS – UML Class Diagram

## USER
- id: int
- name: string
- email: string
- passwordHash: string
- roleId: int

+ Authenticate()
+ ChangePassword()

---

## ROLE
- id: int
- name: string
- permissions: List<string>

---

## CHEMICAL
- id: int
- name: string
- CASNumber: string
- quantity: double
- minimumThreshold: double
- expiryDate: DateTime
- hazardClass: string
- storageLocation: string

+ IsExpired(): bool
+ IsLowStock(): bool
+ IsHazardous(): bool

---

## REQUEST
- id: int
- userId: int
- chemicalId: int
- quantity: double
- status: RequestStatus
- createdAt: DateTime

+ Approve()
+ Reject()
+ Validate()

---

## APPROVAL
- id: int
- requestId: int
- approverId: int
- decision: bool
- comment: string
- timestamp: DateTime

---

## EXPERIMENT
- id: int
- userId: int
- date: DateTime
- chemicalsUsed: List<ChemicalUsage>

+ AddChemical()
+ Confirm()

---

## CHEMICAL USAGE
- chemicalId: int
- quantityUsed: double

---

## EQUIPMENT
- id: int
- name: string
- status: string
- lastMaintenance: DateTime

+ Reserve()
+ Release()
+ NeedsMaintenance()

---

## AUDIT LOG
- id: int
- userId: int
- action: string
- timestamp: DateTime

---

## RELATIONSHIPS

User 1..* → Request  
Request 1..* → Approval  
Experiment *..* → Chemical  
Chemical 1..* → ChemicalUsage  
User 1..1 → Role  
