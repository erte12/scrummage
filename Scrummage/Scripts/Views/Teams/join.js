function bindTeamSearchEvent() {
    var users = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/api/teams/GetTeamsByQuery/%QUERY',
            wildcard: '%QUERY',
            cache: false
        }
    });

    $('#team').typeahead({
            minLength: 1,
            highlight: true
        },
        {
            name: 'teams',
            display: function(team) {
                return team.name;
            },
            source: users
        }).on('typeahead:select',
        function(e, team) {
            console.log(team);
        }).error(function() {
        toastr.error('Something went wrong!');
    });
}