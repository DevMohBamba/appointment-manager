# 📅 Appointment Manager

Application full stack de gestion de rendez-vous avec authentification, construite avec Angular, ASP.NET Core, Entity Framework et SQLite.

## 🚀 Stack technique

- **Frontend** : Angular 16, Bootstrap 5, AuthGuard, HttpInterceptor
- **Backend** : ASP.NET Core 6 Web API
- **Base de données** : SQLite via Entity Framework Core
- **Sécurité** : Authentification JWT
- **Déploiement** : prévu dans un seul projet API + Angular (avec `dotnet publish`)

---

## 🔐 Fonctionnalités principales

### ✅ Authentification
- Inscription (register) avec stockage sécurisé (BCrypt)
- Connexion (login) avec génération de token JWT
- Déconnexion et nettoyage du token
- Intercepteur HTTP pour envoyer automatiquement le token
- Affichage du nom de l'utilisateur connecté
- Protection des routes Angular (AuthGuard)

### 📝 Gestion des rendez-vous
- Affichage de la liste des rendez-vous de l’utilisateur connecté
- Ajout de nouveaux rendez-vous
- Modification via une modale Bootstrap
- Suppression d’un rendez-vous
- Interface responsive en Bootstrap

---

## 🛠️ Installation locale

### Prérequis
- .NET 6 SDK
- Node.js / npm
- Angular CLI (`npm install -g @angular/cli`)

### 🔧 Backend (.NET API)
```bash
cd AppointmentManager.Api
dotnet run
