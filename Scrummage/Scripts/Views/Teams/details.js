function bindUserSearchEvent(teamId) {
    var users = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('name' + 'surname'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/api/users?query=%QUERY&exceptTeamId=@Model.Id',
            wildcard: '%QUERY',
            cache: false
        }
    });

    $('#user').typeahead({
        minLength: 3,
        highlight: true
    }, {
        name: 'users',
        display: function (user) {
            return user.name + ' ' + user.surname;
        },
        source: users
    }).on('typeahead:select', function (e, user) {
        $.ajax({
            url: '/Api/Teams',
            method: 'put',
            data: {
                memberId: user.id,
                teamId: teamId
            }
            }).done(function () {
            toastr.success('New member successfully added.');
            $('#user').typeahead('val', '');
            $('#members').append(
                '<div class="list-group-item">' +
                user.name + ' ' + user.surname +
                '<span data-member-id="' + user.id + '" class="glyphicon glyphicon-remove js-remove pull-right"></span>' +
                '</div>'
            );
        }).error(function () {
            toastr.error('Something went wrong!');
        });
    });
}

function bindUserDeleteEvent(teamId) {
    $('#members').on('click', '.js-remove', function () {
        var memberId = $(this).attr('data-member-id');
        var element = $(this).parent();

        bootbox.confirm('Are you sure?',
            function (result) {
                if (result === true) {
                    $.ajax({
                        url: '/api/teams',
                        method: 'delete',
                        data: { memberId: memberId, teamId: teamId }
                    })
                    .done(function () {
                        toastr.success('Member successfully deleted.');
                        element.remove();
                    })
                    .fail(function () {
                        toastr.error('Something went wrong!');
                    });
                }
            });
    });
}