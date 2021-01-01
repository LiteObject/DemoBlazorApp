namespace DemoBlazorApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.Json.Serialization;
    using System.Threading.Tasks;

    public class UserApiResponse
    {
        [JsonPropertyName("results")]
        public Result[] Results { get; set; }

        [JsonPropertyName("info")]
        public Info Info { get; set; }
    }

    public partial class Info
    {
        [JsonPropertyName("seed")]
        public string Seed { get; set; }

        [JsonPropertyName("results")]
        public long Results { get; set; }

        [JsonPropertyName("page")]
        public long Page { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }
    }

    public partial class Result
    {
        [JsonPropertyName("gender")]
        public string Gender { get; set; }

        [JsonPropertyName("name")]
        public Name Name { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("login")]
        public Login Login { get; set; }

        [JsonPropertyName("dob")]
        public Dob Dob { get; set; }

        [JsonPropertyName("registered")]
        public Dob Registered { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("cell")]
        public string Cell { get; set; }

        [JsonPropertyName("id")]
        public Id Id { get; set; }

        [JsonPropertyName("picture")]
        public Picture Picture { get; set; }

        [JsonPropertyName("nat")]
        public string Nat { get; set; }
    }

    public partial class Dob
    {
        [JsonPropertyName("date")]
        public DateTimeOffset Date { get; set; }

        [JsonPropertyName("age")]
        public long Age { get; set; }
    }

    public partial class Id
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("value")]
        // [JsonConverter(typeof(ParseStringConverter))]
        public string Value { get; set; }
    }

    public partial class Location
    {
        [JsonPropertyName("street")]
        public Street Street { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        /*[JsonPropertyName("postcode")]
        [JsonConverter(typeof(ParseStringConverter))]
        public string Postcode { get; set; } */

        [JsonPropertyName("coordinates")]
        public Coordinates Coordinates { get; set; }

        [JsonPropertyName("timezone")]
        public Timezone Timezone { get; set; }
    }

    public partial class Coordinates
    {
        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }
    }

    public partial class Street
    {
        [JsonPropertyName("number")]
        public long Number { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public partial class Timezone
    {
        [JsonPropertyName("offset")]
        public string Offset { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public partial class Login
    {
        [JsonPropertyName("uuid")]
        public Guid Uuid { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("salt")]
        public string Salt { get; set; }

        [JsonPropertyName("md5")]
        public string Md5 { get; set; }

        [JsonPropertyName("sha1")]
        public string Sha1 { get; set; }

        [JsonPropertyName("sha256")]
        public string Sha256 { get; set; }
    }

    public partial class Name
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("first")]
        public string First { get; set; }

        [JsonPropertyName("last")]
        public string Last { get; set; }

        public string GetFullNameWithTitle()
        {
            return $"{Title}. {First} {Last}";
        }
    }

    public partial class Picture
    {
        [JsonPropertyName("large")]
        public Uri Large { get; set; }

        [JsonPropertyName("medium")]
        public Uri Medium { get; set; }

        [JsonPropertyName("thumbnail")]
        public Uri Thumbnail { get; set; }
    }
}
