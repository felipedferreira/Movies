# Changelog

All notable changes to the Movies project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [0.4.1] - 2026-06-06

### Added
- Initial project setup with Clean Architecture
- Genre enum for movie categorization
- Movies.WebService.Contracts as NuGet package
- Build status badge in README
- Documentation for architecture and folder structure

### Changed
- Organized folder structure with Adapters, Core layers, and Applications

## [Unreleased]

### Added
- Request and response DTOs for API contracts (CreateMoviesRequest, UpdateMoviesRequest, MovieResponse, MoviesResponse)
- RFC 7807 Problem Details standardized error responses in exception handling middleware

### Changed
- Enhanced ExceptionHandlingMiddleware to return structured ProblemDetails format with trace IDs

### Planned
- Complete domain entity implementation
- Repository implementations for PostgreSQL
- API endpoint development
- Integration tests
