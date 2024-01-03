# Pokemon Review API

Welcome to the Pokemon Review API, a powerful tool for retrieving and managing Pokemon reviews. This API allows developers to integrate Pokemon reviews into their applications, websites, or any other projects. Whether you're building a Pokemon fan site, a mobile app, or a data analysis tool, the Pokemon Review API has you covered.

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Usage](#usage)
- [Endpoints](#endpoints)
  - [1. Get All Pokemon](#1-get-all-pokemon)
  - [2. Get Pokemon Rating](#2-get-pokemon-rating)
  - [3. Get Pokemon by ID](#3-get-pokemon-by-id)
  - [4. Add Pokemon](#4-create-pokemon)
  - [5. Delete Pokemon by ID](#5-delete-pokemon-by-id)
  - [6. Update Pokemon by ID](#6-update-pokemon-by-id)
  - [7. Get All Categories](#7-get-all-categories)
  - [8. Get Category by ID](#8-get-category-by-id)
  - [9. Get Pokemons by Category ID](#9-get-pokemons-by-category-id)
  - [10. Add Category](#10-add-category)
  - [11. Delete Category by ID](#5-delete-category-by-id)
  - [12. Update Category by ID](#6-update-category-by-id)
  - [13. Get All Countries](#13-get-all-countries)
  - [14. Get Country by ID](#14-get-country-by-id)
  - [15. Get Owners by Country ID](#15-get-owners-by-country-id)
  - [16. Add Country](#16-add-country)
  - [17. Delete Country by ID](#17-delete-country-by-id)
  - [18. Update Country by ID](#18-update-country-by-id)
  - [19. Get All Owners](#19-get-all-owners)
  - [20. Get Owner by ID](#20-get-owners-by-id)
  - [21. Get Pokemons by Owner ID](#21-get-pokemons-by-owner-id)
  - [22. Add Owner](#22-add-owner)
  - [23. Delete Owner by ID](#23-delete-owner-by-id)
  - [24. Update Owner by ID](#24-update-owner-by-id)
  - [25. Get Reviews](#25-get-reviews)
  - [26. Get Review by ID](#26-get-review-by-id)
  - [27. Get All Pokemon Reviews](#27-get-all-pokemon-reviews)
  - [28. Add a New Pokemon Review](#28-add-a-new-pokemon-review)
  - [29. Update a Pokemon Review](#29-update-a-pokemon-review)
  - [30. Delete a Pokemon Review](#30-delete-a-pokemon-review)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Retrieve Reviews:** Get all Pokemon reviews or fetch a specific review by ID.
- **Add Reviews:** Allow users to add new reviews for their favorite Pokemon.
- **Update Reviews:** Enable users to update their existing reviews.
- **Delete Reviews:** Remove unwanted reviews from the database.

## Getting Started

### Prerequisites

Before you begin, make sure you have the following installed:

- Node.js
- npm (Node Package Manager)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/yousefsaad12/Pokemon-Review-API.git
   ```


3. Set up the environment variables:

   Create a `.env` file in the root directory and add the following:

   ```env
   PORT=3000
   DATABASE_URL=mongodb://localhost:27017/pokemon_reviews
   ```

   Adjust the values as needed.

### Usage


2. The API will be accessible at `http://localhost:3000`.

## Endpoints

### 1. Get All Pokemon Reviews

- **Endpoint:** `GET /reviews`
- **Description:** Retrieve a list of all Pokemon reviews.
- **Parameters:** None
- **Example Response:**
  ```json
  [
    {
      "id": 1,
      "pokemonName": "Pikachu",
      "rating": 4.5,
      "review": "A cute and powerful Pokemon!"
    },
    {
      "id": 2,
      "pokemonName": "Charizard",
      "rating": 5,
      "review": "The best fire-type Pokemon!"
    }
  ]
  ```

### 2. Get Review by ID

- **Endpoint:** `GET /reviews/:id`
- **Description:** Retrieve a specific Pokemon review by its ID.
- **Parameters:**
  - `id` (integer): ID of the desired review.
- **Example Response:**
  ```json
  {
    "id": 1,
    "pokemonName": "Pikachu",
    "rating": 4.5,
    "review": "A cute and powerful Pokemon!"
  }
  ```

### 3. Add a New Pokemon Review

- **Endpoint:** `POST /reviews`
- **Description:** Add a new Pokemon review.
- **Parameters:**
  - `pokemonName` (string): Name of the Pokemon.
  - `rating` (float): Rating given to the Pokemon (1.0 to 5.0).
  - `review` (string): User's review of the Pokemon.
- **Example Request:**
  ```json
  {
    "pokemonName": "Bulbasaur",
    "rating": 4.0,
    "review": "A great starter Pokemon!"
  }
  ```
- **Example Response:**
  ```json
  {
    "id": 3,
    "pokemonName": "Bulbasaur",
    "rating": 4.0,
    "review": "A great starter Pokemon!"
  }
  ```

### 4. Update a Pokemon Review

- **Endpoint:** `PUT /reviews/:id`
- **Description:** Update an existing Pokemon review.
- **Parameters:**
  - `id` (integer): ID of the review to be updated.
  - Any combination of the following fields:
    - `pokemonName` (string): Name of the Pokemon.
    - `rating` (float): Rating given to the Pokemon (1.0 to 5.0).
    - `review` (string): User's review of the Pokemon.
- **Example Request:**
  ```json
  {
    "rating": 4.5,
    "review": "An amazing Pokemon!"
  }
  ```
- **Example Response:**
  ```json
  {
    "id": 3,
    "pokemonName": "Bulbasaur",
    "rating": 4.5,
    "review": "An amazing Pokemon!"
  }
  ```

### 5. Delete a Pokemon Review

- **Endpoint:** `DELETE /reviews/:id`
- **Description:** Delete a specific Pokemon review by its ID.
- **Parameters:**
  - `id` (integer): ID of the review to be deleted.
- **Example Response:**
  ```json
  {
    "message": "Review deleted successfully."
  }
  ```

## Contributing

If you'd like to contribute to the development of the Pokemon Review API, please follow our [Contribution Guidelines](CONTRIBUTING.md).

## License

This project is licensed under the [MIT License](LICENSE). Feel free to use, modify, and distribute the code as per the license terms.
