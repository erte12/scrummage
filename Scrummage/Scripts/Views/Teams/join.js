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
    function (e, team) {
        $.ajax({
            url: '/Api/Teams/SendRequest?id=' + team.id,
            method: 'put'
        }).done(function () {
            toastr.success('New request has been sent.');
            $('#team').typeahead('val', '');
            $('#requestedTeams').append(
                '<div class="list-group-item">' +
                team.name +
                '<span data-team-id="' + team.id + '" class="glyphicon glyphicon-remove js-remove pull-right"></span>' +
                '</div>'
            );
        }).error(function () {
            toastr.error('Something went wrong!');
        });
    }).error(function() {
        toastr.error('Something went wrong!');
    });
}