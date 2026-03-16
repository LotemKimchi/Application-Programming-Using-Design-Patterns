# UML Diagrams — Facebook WinForms App
### Lotem Kimchi 318173481 | Barak Braun 312143274

---

## א. Use Case Diagram

```mermaid
graph LR
    actor["👤\nUser"]

    subgraph system["Facebook WinForms App"]
        direction TB
        UC1(["Login with Facebook"])
        UC2(["Login as Design Patterns"])
        UC3(["Logout"])
        UC4(["View Profile Info"])
        UC5(["View Albums List"])
        UC6(["Find Album with Most Photos"])
        UC7(["Find Oldest Photo"])
        UC8(["Find Most Liked Photo"])
        UC9(["Find Most Commented Photo ★"])
        UC10(["Count Albums ★"])
    end

    fbapi["🔗\nFacebook API"]

    actor --> UC1
    actor --> UC2
    actor --> UC3
    actor --> UC4
    actor --> UC5
    actor --> UC6
    actor --> UC7
    actor --> UC8
    actor --> UC9
    actor --> UC10

    UC1 --> fbapi
    UC2 --> fbapi
    UC3 --> fbapi
    UC4 --> fbapi
    UC5 --> fbapi
    UC6 --> fbapi
    UC7 --> fbapi
    UC8 --> fbapi
    UC9 --> fbapi
    UC10 --> fbapi

    UC4 -.->|include| UC1
    UC5 -.->|include| UC1
    UC6 -.->|include| UC1
    UC7 -.->|include| UC1
    UC8 -.->|include| UC1
    UC9 -.->|include| UC1
    UC10 -.->|include| UC1
```

> ★ = New use case added in this submission

---

## ב. Sequence Diagram 1 — Count Albums (New Use Case)
> Most complex scenario: user is logged in and has albums

```mermaid
sequenceDiagram
    actor User
    participant FormMain
    participant FacebookManager
    participant CountAlbumsFeature
    participant FacebookService

    User->>FormMain: Click "Count Albums" button

    FormMain->>FacebookManager: LoggedInUser
    FacebookManager-->>FormMain: User (not null)

    FormMain->>CountAlbumsFeature: new CountAlbumsFeature()
    FormMain->>CountAlbumsFeature: Execute(user)

    CountAlbumsFeature->>FacebookService: user.Albums (API call)
    FacebookService-->>CountAlbumsFeature: AlbumCollection

    loop for each Album in AlbumCollection
        CountAlbumsFeature->>CountAlbumsFeature: albumsCount++
    end

    CountAlbumsFeature-->>FormMain: int albumsCount

    FormMain->>FormMain: labelAlbumsCount.Text = "Albums Count: N"
```

---

## ב. Sequence Diagram 2 — Most Commented Photo (New Use Case)
> Most complex scenario: user is logged in, has albums with photos that have comments

```mermaid
sequenceDiagram
    actor User
    participant FormMain
    participant FacebookManager
    participant MostCommentedPhotoFeature
    participant FacebookService

    User->>FormMain: Click "Most Commented Photo" button

    FormMain->>FacebookManager: LoggedInUser
    FacebookManager-->>FormMain: User (not null)

    FormMain->>FormMain: labelMostCommentedStatus.Visible = false
    FormMain->>FormMain: pictureBoxMostCommentedPhoto.ImageLocation = null

    FormMain->>MostCommentedPhotoFeature: new MostCommentedPhotoFeature()
    FormMain->>MostCommentedPhotoFeature: Execute(user)

    MostCommentedPhotoFeature->>FacebookService: user.Albums (API call)
    FacebookService-->>MostCommentedPhotoFeature: AlbumCollection

    loop for each Album in AlbumCollection
        MostCommentedPhotoFeature->>FacebookService: album.Photos (API call)
        FacebookService-->>MostCommentedPhotoFeature: PhotoCollection

        loop for each Photo in PhotoCollection
            MostCommentedPhotoFeature->>FacebookService: photo.Comments (API call)
            FacebookService-->>MostCommentedPhotoFeature: int commentCount

            alt commentCount > maxComment
                MostCommentedPhotoFeature->>MostCommentedPhotoFeature: mostCommentedPhoto = photo
                MostCommentedPhotoFeature->>MostCommentedPhotoFeature: maxComment = commentCount
            end
        end
    end

    MostCommentedPhotoFeature-->>FormMain: Photo (mostCommentedPhoto)

    alt photo != null
        FormMain->>FormMain: pictureBoxMostCommentedPhoto.ImageLocation = photo.PictureNormalURL
        FormMain->>FormMain: labelMostCommentedStatus.Text = "N comments"
        FormMain->>FormMain: labelMostCommentedStatus.Visible = true
    else photo == null
        FormMain->>FormMain: labelMostCommentedStatus.Text = "No photo found"
        FormMain->>FormMain: labelMostCommentedStatus.Visible = true
    end
```

---

## ג. Class Diagram

```mermaid
classDiagram
    direction TB

    class FormMain {
        -string k_AppId
        -string k_DesignPatternsToken
        -FacebookManager m_FacebookManager
        +FormMain()
        -ensureLoggedIn() bool
        -afterLogin() void
        -displayUserInfo() void
        -connectWithSavedToken() void
        -buttonLogin_Click(sender, e) void
        -buttonLogout_Click(sender, e) void
        -buttonConnectAsDesig_Click_1(sender, e) void
        -buttonMostPhotosAlbum_Click(sender, e) void
        -buttonOldestPhoto_Click_1(sender, e) void
        -buttonMostLikedPhoto_Click_1(sender, e) void
        -buttonMostCommentedPhoto_Click_1(sender, e) void
        -buttonCountAlbums_Click(sender, e) void
    }

    class FacebookManager {
        -LoginResult m_LoginResult
        +User LoggedInUser
        +Login(i_AppId : string) LoginResult
        +ConnectWithSavedToken() LoginResult
        +ConnectWithToken(i_Token : string) LoginResult
        +Logout() void
    }

    class IFacebookFeature~T~ {
        <<interface>>
        +Execute(i_User : User) T
    }

    class AlbumWithMostPhotosFeature {
        +Execute(i_User : User) Album
    }

    class OldestPhotoFeature {
        +Execute(i_User : User) Photo
    }

    class MostLikedPhotoFeature {
        +Execute(i_User : User) Photo
    }

    class MostCommentedPhotoFeature {
        +Execute(i_User : User) Photo
    }

    class CountAlbumsFeature {
        +Execute(i_User : User) int
    }

    class FacebookService {
        <<external>>
        +Login(appId : string, permissions : string[]) LoginResult$
        +Connect(token : string) LoginResult$
        +LogoutWithUI() void$
        +s_CollectionLimit : int$
    }

    class LoginResult {
        <<external>>
        +string AccessToken
        +string ErrorMessage
        +User LoggedInUser
    }

    class User {
        <<external>>
        +string Name
        +string Birthday
        +string PictureNormalURL
        +FacebookObjectCollection~Album~ Albums
    }

    class Album {
        <<external>>
        +string Name
        +FacebookObjectCollection~Photo~ Photos
    }

    class Photo {
        <<external>>
        +string PictureNormalURL
        +DateTime CreatedTime
        +FacebookObjectCollection~Like~ LikedBy
        +FacebookObjectCollection~Comment~ Comments
    }

    %% FormMain owns FacebookManager (composition — lifetime tied to form)
    FormMain *-- FacebookManager : composition

    %% FormMain depends on features transiently (creates and discards per click)
    FormMain ..> AlbumWithMostPhotosFeature : creates
    FormMain ..> OldestPhotoFeature : creates
    FormMain ..> MostLikedPhotoFeature : creates
    FormMain ..> MostCommentedPhotoFeature : creates
    FormMain ..> CountAlbumsFeature : creates

    %% FormMain programs to the interface
    FormMain ..> IFacebookFeature~T~ : uses

    %% All feature classes implement the interface
    AlbumWithMostPhotosFeature ..|> IFacebookFeature~T~ : implements
    OldestPhotoFeature ..|> IFacebookFeature~T~ : implements
    MostLikedPhotoFeature ..|> IFacebookFeature~T~ : implements
    MostCommentedPhotoFeature ..|> IFacebookFeature~T~ : implements
    CountAlbumsFeature ..|> IFacebookFeature~T~ : implements

    %% FacebookManager uses FacebookService (dependency — calls static methods)
    FacebookManager ..> FacebookService : uses

    %% FacebookManager aggregates LoginResult (holds reference, does not create User)
    FacebookManager o-- LoginResult : aggregates

    %% LoginResult contains User reference
    LoginResult --> User : contains

    %% Domain model associations
    User "1" --> "*" Album : has
    Album "1" --> "*" Photo : contains

    %% Feature classes depend on domain model
    AlbumWithMostPhotosFeature ..> User : reads
    AlbumWithMostPhotosFeature ..> Album : reads
    OldestPhotoFeature ..> User : reads
    OldestPhotoFeature ..> Album : reads
    OldestPhotoFeature ..> Photo : reads
    MostLikedPhotoFeature ..> User : reads
    MostLikedPhotoFeature ..> Album : reads
    MostLikedPhotoFeature ..> Photo : reads
    MostCommentedPhotoFeature ..> User : reads
    MostCommentedPhotoFeature ..> Album : reads
    MostCommentedPhotoFeature ..> Photo : reads
    CountAlbumsFeature ..> User : reads
    CountAlbumsFeature ..> Album : reads
```
