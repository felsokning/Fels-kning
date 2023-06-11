# Felsökning NuGet Package
The Felsökning NuGet Package is, generally, the base package used/consumed/referenced by all other Felsökning NuGet packages.

## `AggregateExceptionExtensions`
This static class contains extension methods for the [AggregateException](https://learn.microsoft.com/en-us/dotnet/api/system.aggregateexception) class.

### Methods

#### `Unbox()`
##### Definition
The `Unbox` method is used to recursively unbox the nested child exceptions within an AggregateException.
##### Returns
 A `string[]`, in which `string[0]` is the hResults, `string[1]` is the exception messages, and `string[2]` is the stack traces.

## `CollectionExtensions`
This static class contains extension methods for the [`ICollection<T>`], [`IEnumerable<T>`], and [`IList<T>`] interfaces.

### Methods

#### `ToIAsyncEnumerable<T>()`
##### Definition
Extends the [`ICollection<T>`], [`IEnumerable<T>`], and/or [`IList<T>`] interfaces to return an [`IAsyncEnumerable{T}`](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.iasyncenumerable-1), which the iteration can be awaited through.
##### Returns
An `IAsyncEnumerable{T}`, which can be awaited through iteration.

## `DateTimeExtensions`
This static class contains extensions extension methods for the `DateTime` struct.

### Methods

#### `IsWeekDay()`
##### Definition
Extends the `DateTime`struct to determine if the given `DateTime` object falls/occurs on a weekday in the Gregorian Calendar.

##### Returns
`true` if the given day is a weekday, otherwise `false`.

#### `ToCulturedString(string culture)`
##### Definition
Extends the `DateTime` struct to return a standardised string based on the passed culture.

##### Returns
A string representation of the `DateTime` struct in the culture-based format.

#### `ToIso8601UtcString()`
##### Definition
Extends the `DateTime` struct to convert the given `DateTime` first to UTC Time Zone and then to the [ISO:8601](https://www.iso.org/iso-8601-date-and-time-format.html) `string` representation.

##### Returns
A string containing the `ISO:601` standardised value for the `DateTime` object.

#### `ToPosixTime()`
##### Defintion
Extends the `DateTime` struct to convert the given `DateTime` to the [POSIX time](https://pubs.opengroup.org/onlinepubs/9699919799/xrat/V4_xbd_chap04.html) representation.

##### Returns
A `long` representing the `DateTime` object in POSIX time representation.

#### `ToRfc1123String()`
##### Definition
Extends the `DateTime` struct to convert the given `DateTime` to the [RFC:1123](https://www.rfc-editor.org/rfc/rfc822#section-5) `string` representation.

##### Returns
A `string` representing the `DateTime` object in RFC:1123 time representation.

#### `ToUnixEpochTime()`
Note: Duplicates `ToPosixTime()`
##### Definition
Extends the `DateTime` struct to convert the given `DateTime` to the [POSIX time](https://pubs.opengroup.org/onlinepubs/9699919799/xrat/V4_xbd_chap04.html) representation.

##### Returns
A `long` representing the `DateTime` object in POSIX time representation.

#### `ToWeekNumber()`
##### Definition
Extends the `DateTime` struct to covert the given `DateTime` to the ISO:8601 week date, which is the given week number of the year.
##### Returns
An `int` representing the given week number of the year for the given `DateTime`.

## `EncryptedFile` Class
### Definition 
A class for ensuring System.IO.File objects are encrypted on entering `.Dispose()`.

NOTE: Only supported on Windows Systems, Professional.

### Methods
`DecryptAndOpen()`
#### Definition
Decrypts the file (if encrypted) and returns the [FileStream](https://learn.microsoft.com/en-us/dotnet/api/system.io.filestream) from opening the file.