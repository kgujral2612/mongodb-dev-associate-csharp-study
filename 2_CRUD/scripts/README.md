# Practice Questions
## sample_mflix dataset

https://www.mongodb.com/docs/atlas/sample-data/sample-mflix/

Q1. Count the number of movies
`db.movies.countDocuments()`

Q2. Display the title and the awards of the five oldest released movies
`db.movies.find({}, {_id:0, title:1, awards:1}).sort({ year: 1}).limit(5)`

Q3. Display five movies that have won the most number of awards
`db.movies.find().sort({ "awards.wins": -1 }).limit(5)`

Q4. Display count of short documentaries
`db.movies.countDocuments({ genres : {$all : ['Short', 'Documentary']}  })`
`db.movies.find({ genres: { $all: ["Documentary", "Short"] } }).count()`
Note the use of the `$all` operator instead of the `$in`.

Q5. Display all titles of movies with more than 3 awards in the adventure category.
```
db.movies.find({
 "awards.wins" : { $gte : 3},
 "genres" : "Adventure"
 },
 { _id: 0, title: 1})
```
```
db.movies.find({
 "awards.wins" : { $gte : 3},
 "genres" : { $elemMatch: { $eq: "Adventure" }}
 },
 { _id: 0, title: 1})
```
Q6. Create a pipeline to find the top 3 highest-rated movies released before 1920.
```
db.movies.aggregate([
  {
    $match: {
      year: {
        $lt: 1920,
      },
    },
  },
  { $sort: { "imdb.rating": -1, }},
  { $limit: 3 },
  {
    $project: {
      _id: 0,
      title: 1,
      year: 1,
      "imdb.rating": 1,
    },
  },
])
```

Q7. Display avg number of awards won, in each movie category.
TODO

Q8. Display 5 latest comments
`db.comments.find().sort( { date: -1} ).limit(5)`

Q9. Display the name of the user with highest number of comments.
```
db.comments.aggregate([
    {
        $group: {
            _id: "$name",
            commentsCount: { $count: { } }
        }
    },
    {
        $sort: { "commentsCount" : -1}
    },
    {
        $limit: 1
    }
])
```
OR
```
db.comments.aggregate([
    {
        $group: {
            _id: "$name",
            commentsCount: { $sum : 1 }
        }
    },
    {
        $sort: { "commentsCount" : -1}
    },
    {
        $limit: 1
    }
])
```
Q10. For each year, display title of the movie that has won the highest number of awards.
TODO
