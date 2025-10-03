# R5.A.08 - Qualité de développement

## Comment lancer le projet en local ?

- Installer Dotnet.
    ```
    dotnet tool install --global dotnet-ef --version 8.0.11
    ```

- Mettre à jour la base de données (peut la créer automatiquement).
    ```
    dotnet-ef database update --project R5A08_TP1
    ```

## Versions des dépendances.

- API.
    - AutoMapper.Extensions.Microsoft.DependencyInjection (12.0.1).
    - Microsoft.EntityFrameworkCore.Tools (8.0.11).
    - Microsoft.VisualStudio.Web.CodeGeneration.Design (8.0.7).
    - Npgsql.EntityFrameworkCore.PostgreSQL (8.0.11).
    - Swashbuckle.AspNetCore (6.6.2).

- Tests.
    - Microsoft.NET.Test.Sdk (17.12.0).
    - Moq (4.20.72).
    - MSTest (3.6.4).

- Blazor.
    - Blazor.Bootstrap (3.4.0).
    - CommunityToolKit.Mvvm (8.4.0).
    - Microsoft.AspNetCore.Components.WebAssembly (8.0.17).
    - Microsoft.AspNetCore.Components.WebAssembly.DevServer (8.0.17).
    - Microsoft.Playwright.MSTest (1.55.0).
    - MSTest.TestAdapter (3.1.1).
    - MSTest.TestFramework (3.1.1).

## Base de données.

- Utilise Npgsql.
- Serveur de la base de données : localhost.
- Port de la base de données : 5432.
- Nom de la base de données : R508.

## ❔ 3 questions sur le projet.
### Quels principes SOLID le code de votre API REST respecte-t-il et lesquels ne respecte-t-il pas ?

    ✅ Single Responsibility Principle.
    ✅ Open/Closed Principle.
    ✅ Liskov Substitution Principle.
    ✅ Interface Segregation Principle.
    ✅ Dependency Inversion Principle.

### Si vous ne les avez pas appliqués dans votre code, quelles améliorations pouvez-vous mettre en place pour améliorer la qualité du code ?

- Le TP nous demande d'utiliser une version d'AutoMapper qui est maintenant ```Deprecated```, il aurait été intéressant de pouvoir utiliser la version officielle qui a intégré cette fonctionnalité de Dependency Injection.

- Pour rendre le Data Binding encore plus efficace, joli et robuste, on aurait pu utiliser l'interface ```INotifyPropertyChanged```.

- Avec plus de temps, l'application web aurait pu être plus remplie et plus de choses aurait été testé, comme l'inspection d'un produit, la vérification de chaque champ avec un message d'erreur personnalisé, de l'auto-complétion sur tous les filtres, etc.

- Une entité image.

- Encore plus de tests d'intégrations pour tester certaines méthodes (ShouldNotCreate par exemple).

- Des tests unitaires dans le Blazor.
    
### 📈 Si vous avez appliqué des améliorations dans votre code, lesquelles sont-elles et que permettent-elles d’améliorer en termes de qualité ?

1. API. 📲

    - Utilisation du **Pattern Repository**, cela ajoute une nouvelle couche dans notre architecture directement entre la couche métier (avec notre Manager) et la base de données. 
    Entre autres, cette amélioration respecte 2 principes SOLID: 
        - **Single Responsibility Principle:** un repository a une seule responsabilité : gérer les opérations de base pour une entité, il ne s'occupe pas des détails métiers. La classe métier ne gère plus l'accès aux données.
        - **Dependency Inversion Principle:** plutôt que d'écrire une classe qui dépend d'une autre classe, on fait dépendre la classe d'une interface.
        - **Liskov Substitution Principle:** encourage l'utilisation d'interfaces ou d'abstractions plutôt que de classes concrètes. Cela permet de substituer facilement une implémentation par une autre.

    - Utilisation de la **Dependency Injection**, cela permet aux classes de ne plus créer ses dépendances, mais de les recevoir. A chaque fois que ```IGetDataRepository<TEntity>``` est utilisée une instance de ```GetDataManager<TEntity>``` est recréer, l'injection de dépendance permet donc de rendre possible le Pattern Repository.  
    La DI aide à respecter plusieurs principes SOLID, en particulier : 
        - **Single Responsibility Principle:** en utilisant l'injection de dépendance une classe ne crée pas ou ne gère pas elle-même ses dépendances. Cela réduit la responsabilité de la classe, qui se concentre uniquement sur sa logique métier.
        - **Open/Closed Principle:** les dépendances peuvent être injectées sans modifier la classe qui les utilise.

    - L'utilisation de **TEntity** permet d'écrire une seule classe ou méthode qui fonctionne avec n'importe quel type d'entité par exemple un repository générique.  
    Elle respecte les principes SOLID suivant:
        - **Open/Closed Principle:** car l'interface est ouverte à l'extension (utilisation par n'importe quelle entité), mais fermée à la modification (pas besoin de modifier l'interface pour chaque nouveau type).
        - **Dependency Inversion Principle:** en injectant ```IGetDataRepository<TEntity>``` dans les services métiers (Managers) ils dépendront d'abstractions et non d'implémentations.
    
    - **L'utilisation d'interfaces** à la place de classes permet de ne pas dépendre aux changements apportés à ses dernières.  
    Elle respecte les principes SOLID suivant:
        - **Liskov Substitution Principle:** en utilisant une interface générique on respecte le principe de substitution, n'importe quel entité peut être substitué sans casser la logique ou le code.
        - **Dependency Inversion Principle:** dépend d'abstractions et non d'implémentations, favorisant la modularité, flexibilité et la réutilisabilité en réduisant les dépendances directes entre les modules.  
        Exemple: au lieu d'utiliser ```List<Product>``` on utilise l'interface ```ICollection<Product>```.

    - L'ajout de **classes partielles** permet de laisser le modèle utilisé par EntityFramework pour le mapping propre. De plus, si les modèles EntityFramework étaient générés par Scaffolding alors les classes partielles permettraient de conserver les ajouts même si les classes générées sont écrasées.  
    Elle respecte le principe SOLID suivant:
        - **Single Responsibility Principle:** la logique métier est séparée dans un autre fichier, ce qui permet d'améliorer la lisibilité et la maintenabilité.

    - Application du **pattern d'architecture CQRS** (Command Query Responsibility Segregation), cela consiste à séparer les opérations de lecture (Query) et d'écriture (Command) sur les données. Dans l'API, les managers et les interfaces ont été conçues de façon à respecter ce pattern d'architecture CQRS.  
    Pourquoi séparer les deux ?
        - **L'optimisation:** Les besoins de lecture et d'écriture sont très différents. Par exemple la lecture peut demander des vues rapides tandis que l'écriture doit garantir la cohérence des données.
        - **La sécurité:** On peut appliquer des règles différentes selon qu'on lit ou qu'on écrit.
        - **Maintenabilité:** Le code est plus clair car chaque partie a une responsabilité précise (Single Responsability Principle). 
        - **Principe SOLID:** De plus ce code permet de respecter le principe SOLID suivant: Interface Segregation Principle. Il vaut mieux avoir plusieurs interfaces spécifiques qu'une seule interface générale.  

    - Utilisation du **Async** avec le Task, permet d'avoir de meilleures performances et de fluidité (pas de blocage).
        ```cs
        public async Task<ActionResult<IEnumerable<Product>>> GetAllWithIncludesAsync()
        {
            return await products
                .Include(p => p.BrandNavigation)
                .Include(p => p.ProductTypeNavigation)
                .ToListAsync();
        }
        ```

    - **Tests d'intégrations et Mock:** environ 11 tests d'intégration et 11 tests Mock par contrôleur, rendant le total de tests au nombre de 64. Un Pattern classique est suivi, un test qui réussi et un test qui échoue au moins par réponse HTTP. Les asserts vérifient le type de retour, si le result est nul, si les valeurs correspondent, etc.

    - Afin de factoriser le code dans les tests d'intégration et Mock l'utilisation du **TestInitialize** et le **TestCleanup** est nécessaire. Il a permet d'instancier qu'une seule fois certains objets, et de les réutiliser tout au long de nos tests, ce qui évite de les instancier à chaque test. Une **couverture de code** aurait pu être faite pour pouvoir confirmer s'il manquait des méthodes à tester.

    - Une **documentation Swagger** a été réalisé sur chaque réponse HTTP de chaque contrôleur. Comme par exemple:
        ```cs
        /// <summary>
        /// Récupère un produit avec son id.
        /// </summary>
        /// <param name="id">L'id du produit.</param>
        /// <returns>Un produit sous forme de réponse HTTP 200 OK.</returns>
        /// <response code="200">Le produit a été récupéré avec succès.</response>
        /// <response code="404">Le produit demandé n'existe pas.</response>
        /// <response code="500">Une erreur interne s'est produite sur le serveur.</response>
        // GET: Products/GetById/5
        [HttpGet]
        [Route("[action]/{id}")]
        [ActionName("GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        ```

    - Application du **CodeFirst** dans les Models EntityFramework afin de pouvoir générer la base de données. Cela permet d'adapter la base de données aux besoins qui sont définis dans l'API et pas l'inverse. De plus, afin de normaliser les noms de colonnes dans la base de données, la **norme ISO/IEC 9075 (SQL)** a été appliqué.

    - Afin de simplifier et optimiser le rendu / envoi de données, l'API utilise des **DTO** avec l'aide de l'extension **AutoMapper**. Cela permet d'afficher ou d'envoyer que les champs qui sont vraiment utiles ou nécessaire selon le contexte. 

2. Application web (Blazor). 💻

    - L'application Blazor a été conçu en **MVVM** (Model -> View -> ViewModel), une amélioration du modèle classique MVC (Model -> View -> Controller). Le MVVM est considéré comme une amélioration au MVC car il permet de faire du **Data Binding** (liaison automatique) sans avoir besoin d'écrire du code pour rafraîchir les données. Le MVVM sépare beaucoup mieux le code, la View n'a quasiment pas de logique, elle est gérée dans le ViewModel.

    - Grâce à l'utilisation du MVVM, nous utilisons **l'injection de dépendance** pour utiliser les ViewModel dans les View. Elle est aussi utilisée par le WebService (la classe qui va utiliser notre API) qui hérite d'une interface.

    - L'application utilise plusieurs composants de **Bootstrap** comme les boutons, les notifications, l'auto-complétion, etc.
        ```cs
        <ConfirmDialog @ref="dialog" />
        ```
        ```cs
        <Toasts class="p-3" Messages="messages" Placement="ToastsPlacement.TopRight" />
        ```
        ```cs
        <AutoComplete @bind-Value="ViewModel.SelectedProductName"
              TItem="Product"
              DataProvider="ProductsDataProvider"
              PropertyName="NameProduct"
              Placeholder="Search a product..."
              OnChanged="OnAutoCompleteChanged" />
        ```
        ```cs
        <Button Color="ButtonColor.Danger" @onclick="() => ShowConfirmationAsync(product.IdProduct)"> Delete </Button>
        ```    

    - Des **tests End To End** (non testé à car ils ne se lance pas pour une raison obscure) afin de tester les fonctionnalités de chaque page.

## 5️⃣ Les 5 principes SOLID.

- S - Single Responsibility Principle (SRP).
> "Une classe doit avoir une seule responsabilité, une seule raison de changer."

- O - Open/Closed Principle (OCP).
> "Les classes doivent être ouvertes à l’extension, mais fermées à la modification."

- L - Liskov Substitution Principle (LSP).
> "Les objets d'une classe dérivée doivent pouvoir remplacer ceux de leur classe de base sans altérer la logique."

- I - Interface Segregation Principle (ISP).
> "Il vaut mieux plusieurs interfaces spécifiques qu’une seule interface générale."

- D - Dependency Inversion Principle (DIP)
> "Dépendre d’abstractions, pas de classes concrètes."

## Autre.

### Si besoin d'aide.

- Si le lancement de l'API ou de l'application ne marche pas, bien vérifier qu'ils sont en http (et pas https), cela vaut surtout pour l'API.

### Mainteneur.

- BARRIER Florian, Florian.Barrier@etu.univ-smb.fr
