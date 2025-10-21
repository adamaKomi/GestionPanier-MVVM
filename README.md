# GestionPanier

Une application web ASP.NET Core Razor Pages pour la gestion d'un panier d'achats avec persistance via cookies.

## 📋 Description

GestionPanier est une application de commerce permettant aux utilisateurs de parcourir des produits, gérer un panier d'achats. L'application utilise Entity Framework Core pour la persistance des données et stocke le panier dans des cookies pour une expérience utilisateur fluide.

## 🚀 Technologies

- **.NET 9.0**
- **C# 13.0**
- **ASP.NET Core Razor Pages**
- **Entity Framework Core**
- **Bootstrap** (pour l'interface utilisateur)
- **jQuery Validation** (pour la validation côté client)

## 🏗️ Architecture

### Modèles de données

- **Produit** : Représente les produits disponibles
  - `Id`, `Nom`, `Prix`, `Stock`
  - Validation avec Data Annotations
  
- **CartItemViewModel** : Représente un article dans le panier
  - `ProductId`, `ProductName`, `UnitPrice`, `Quantity`

### Pages Razor

#### Panier.cshtml / CartModel
Page principale de gestion du panier avec les fonctionnalités suivantes :

- **OnGet()** : Charge le panier depuis les cookies et calcule le total
- **OnPostUpdateQuantities()** : Met à jour les quantités des produits
- **OnPostRemove(int productId)** : Supprime un produit spécifique
- **OnPostClearCart()** : Vide complètement le panier
- **OnPostAddAsync(int productId)** : Ajoute un produit au panier

### Contexte de base de données

**GestionPanierContext** : DbContext Entity Framework Core
- `DbSet<Produit> Produit`

### Helpers

**CookieCartHelper** : Classe utilitaire pour gérer la persistance du panier dans les cookies
- `GetCartCookie(HttpContext)` : Récupère le panier depuis le cookie
- `SetCartCookie(HttpContext, List<CartItemViewModel>)` : Sauvegarde le panier dans le cookie

## 🔧 Fonctionnalités

### ✅ Implémentées

- ✅ Affichage des produits
- ✅ Ajout de produits au panier
- ✅ Modification des quantités dans le panier
- ✅ Suppression d'articles du panier
- ✅ Calcul automatique du total
- ✅ Persistance du panier via cookies
- ✅ Logging détaillé des opérations
- ✅ Validation des données avec Data Annotations
- ✅ Interface responsive avec Bootstrap

### 🚧 En développement


## 📦 Installation

1. **Cloner le repository**
```sh
git clone <repository-url>
   cd GestionPanier
```

2. **Configurer la base de données**
   
   Mettre à jour la chaîne de connexion dans `appsettings.json`

3. **Appliquer les migrations**
```sh
dotnet ef database update
```

4. **Lancer l'application**
```sh
dotnet run
```

## 🔐 Sécurité

### Validation des données

- **Produits** : Validation du nom (requis), prix (1-1000000), stock (≥0)
  
### Authentification

Le système d'authentification est prévu mais actuellement désactivé. Le décorateur `[Authorize]` est commenté sur la page `CartModel`.

## 📝 Structure du projet

```
GestionPanier/
├── Data/
│   └── GestionPanierContext.cs    # Contexte EF Core
├── Models/                         # Modèles de données
│   ├── Produit.cs
├── Pages/
│   ├── Panier.cshtml              # Vue du panier
│   ├── Panier.cshtml.cs           # Logic du panier
│   └── Index.cshtml               # Page d'accueil (produits)
├── Extensions/
│   └── CookieCartHelper.cs        # Gestion des cookies de panier
└── wwwroot/                        # Ressources statiques
    └── lib/
        ├── bootstrap/
        └── jquery-validation/
```

## 🛠️ Configuration

### Cookies de panier

Le panier est stocké sous forme de JSON sérialisé dans un cookie. La structure est une liste de `CartItemViewModel`.
