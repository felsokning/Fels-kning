# Felsökning NuGet Package

The Felsökning NuGet Package is the base library used by other Felsökning NuGet packages. It provides a variety of utility methods and extensions to simplify common programming tasks.

## Features

- Extensions for `AggregateException`, `DateTime`, and collection interfaces.
- Utilities for working with encrypted files.
- HTTP-related base classes and extensions.

---

## `AggregateExceptionExtensions`

This static class contains extension methods for the [AggregateException](https://learn.microsoft.com/en-us/dotnet/api/system.aggregateexception) class.

### Methods

#### `Unbox()`
- **Definition**: Recursively unboxes the nested child exceptions within an `AggregateException`.
- **Returns**: A `string[]` where:
  - `string[0]`: hResults.
  - `string[1]`: Exception messages.
  - `string[2]`: Stack traces.

---

## `CollectionExtensions`

This static class contains extension methods for the [`ICollection<T>`], [`IEnumerable<T>`], and [`IList<T>`] interfaces.

### Methods

#### `ToIAsyncEnumerable<T>()`
- **Definition**: Converts a collection or enumerable to an [`IAsyncEnumerable<T>`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1), allowing asynchronous iteration.
- **Returns**: An `IAsyncEnumerable<T>`.

---

## `DateTimeExtensions`

This static class contains extension methods for the `DateTime` struct.

### Methods

#### `IsWeekDay()`
- **Definition**: Determines if the given `DateTime` occurs on a weekday in the Gregorian calendar.
- **Returns**: `true` if the day is a weekday, otherwise `false`.

#### `ToCulturedString(string culture)`
- **Definition**: Converts the `DateTime` to a culture-specific string format.
- **Returns**: A `string` representation of the `DateTime` in the specified culture format.

#### `ToIso8601UtcString()`
- **Definition**: Converts the `DateTime` to UTC and formats it as an [ISO:8601](https://www.iso.org/iso-8601-date-and-time-format.html) string.
- **Returns**: A `string` in ISO:8601 format.

#### `ToPosixTime()`
- **Definition**: Converts the `DateTime` to [POSIX time](https://pubs.opengroup.org/onlinepubs/9699919799/xrat/V4_xbd_chap04.html).
- **Returns**: A `long` representing the `DateTime` in POSIX time.

#### `ToRfc1123String()`
- **Definition**: Converts the `DateTime` to an [RFC:1123](https://www.rfc-editor.org/rfc/rfc822#section-5) string.
- **Returns**: A `string` in RFC:1123 format.

#### `ToUnixEpochTime()`
- **Definition**: Converts the `DateTime` to Unix epoch time. (Duplicate of `ToPosixTime()`.)
- **Returns**: A `long` representing the `DateTime` in Unix epoch time.

#### `ToWeekNumber()`
- **Definition**: Converts the `DateTime` to the ISO:8601 week number of the year.
- **Returns**: An `int` representing the week number.

---

## `EncryptedFile` Class

A class for ensuring `System.IO.File` objects are encrypted when `.Dispose()` is called.

### Methods

#### `DecryptAndOpen()`
- **Definition**: Decrypts the file (if encrypted) and opens it as a [FileStream](https://learn.microsoft.com/en-us/dotnet/api/system.io.filestream).
- **Returns**: A `FileStream` object.

---

## `HttpBase`

A base class for any class that depends on `HttpClient`. The `HttpClient` is initialized with TLS 1.3 and default headers, such as `X-Correlation-ID`.

---

## `HttpExtensions`

This static class contains extensions for performing HTTP-related tasks, such as `GET`, `POST`, and `PUT` requests, with automatic deserialization of responses.

### Example

```csharp
var forecasts = await HttpExtensions.GetAsync<ForecastsForRegion>($"https://www.met.ie/Open_Data/json/{region}.json");