<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="SimpleWebsite.Handlers.Movies.List" %>
<%@ Import Namespace="SimpleWebsite.Handlers.Movies"%>
<%@ Import Namespace="FubuMVC.Core.Urls"%>
<%@ Import Namespace="SimpleWebsite.Core"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>My Movies</title>
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.0/jquery.min.js"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.7.2/jquery-ui.min.js"></script>
    <style type="text/css">
body
{
    background-color: Red;
    font-family:arial,helvetica,sans-serif;
}

.content
{
    background-color: White;
    height: 400px;
    width: 600px;
    margin-left:auto;
    margin-right:auto;
    padding: 0px 4px 2px 4px;
}

.removeLink
{
    margin-left:3px;
}
    </style>
</head>
<body>

    <div class="content">
        <h2>Movies I want to see</h2>

        <div>
            <input name="newMovieTitle" id="newMovieTitle" />
            <input type="button" value="Add" id="addMovieButton" />
        </div>
        <ul id="movieList"></ul>
    </div>
</body>

<script type="text/javascript">

    var addMovieUrl = "<%= Get<IUrlRegistry>().UrlFor(new AddMovieInput()) %>";
    var removeMovieUrl = "<%= Get<IUrlRegistry>().UrlFor(new RemoveMovieInput()) %>";
    var movies = <%= Model.Movies.ToJson() %>;

    $(document).ready(function(){
        var movieList = $("#movieList");

        var addMovieToList = function(movie){
            var listItem = $("<li>").text(movie.Title);
            listItem.append( $("<a>").text("x")
                .attr("href", "#")
                .addClass("removeLink")
                .data("movieId", movie.Id) );
            movieList.append( listItem );
        };
        
        var saveNewMovie = function(){
            var title = $("#newMovieTitle").val();
            $.post(addMovieUrl, {Title: title}, saveMovieResponse, "json");
        };
        
        var saveMovieResponse = function(data){
            if (data.Success !== true) {
                alert("failed to add your movie");
                return;
            }
            
            $("#newMovieTitle").val("");
            addMovieToList(data.Item);
        };
        
        $.each(movies, function(i, elem){
            addMovieToList(elem);
        });
        
        $(".removeLink").live("click", function(){
            var link = $(this);
            var movieId = link.data("movieId");
            
            var onSuccess = function(data){
                if (data.Success !== true){
                    alert("failed to remove");
                    return;
                }
                
                var listItem = link.parent("li");
                listItem.remove();
            }
            
            $.post(removeMovieUrl, {Id: movieId}, onSuccess, "json");
        });
        
        $("#addMovieButton").click(saveNewMovie);
    });


</script>
</html>
