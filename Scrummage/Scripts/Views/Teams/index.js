var source = $('#teamRow').html();
var template = Handlebars.compile(source);
var table = $('#teamsTable');

function loadTeamsTableData() {
    $.get('/Api/Teams', function (data) {
        data.forEach(function (team) {
            var teamRowHtml = template(team);
            $('#teamsTable tbody').append(teamRowHtml);
        });
    }).done(function () {
        table = table.DataTable();
    });
}

function bindTeamCreateEvent() {
    $('#newTeamButton').on('click', function () {
        bootbox.prompt({
            size: "small",
            title: "Team name:",
            callback: function (name) {
                if (name !== null) {
                    $.ajax({
                            url: '/Api/Teams',
                            method: 'post',
                            data: { name: name }
                        })
                        .done(function (newTeam) {
                            toastr.success('New team has been created!');
                            var teamRowHtml = template(newTeam);
                            table.row.add($(teamRowHtml)).draw();

                            var teamsListElement = $('<option value="' + newTeam.id + '">' + newTeam.name + '</option>');
                            teamsListElement.appendTo('#teams');
                            //TODO: Validation
                        })
                        .fail(function () {
                            toastr.error('Something went wrong!');
                        });
                }
            }
        });
    });

    $('#newTeam').validate({
        submitHandler: function () {
            $.ajax({
                    url: '/Api/Teams',
                    method: 'post',
                    data: $('#newTeam').serialize()
                })
                .done(function (newTeam) {
                    toastr.success('New team has been created!');
                    $('#newTeam').find('input').val('');

                    //TODO: Insert new team to table
                })
                .fail(function () {
                    toastr.error('Something went wrong!');
                });

            return false;
        }
    });
}

function bindTeamDeleteEvent()
{
    $('#teamsTable tbody').on('click', '.js-delete', function () {

        var teamRow = $(this).parents('tr');
        var teamId = teamRow.attr('data-team-id');

        bootbox.confirm({
            title: "Delete team?",
            message: "Do you want to delete this team? This cannot be undone.",
            buttons: {
                cancel: {
                    label: '<i class="fa fa-times"></i> Cancel'
                },
                confirm: {
                    label: '<i class="fa fa-check"></i> Confirm'
                }
            },
            callback: function (result) {
                if (result === true) {
                    $.ajax({
                            url: '/Api/Teams/' + teamId,
                            method: 'delete'
                        })
                        .done(function () {
                            toastr.success('Team successfully deleted');
                            table.rows(teamRow).remove().draw();
                        })
                        .fail(function () {
                            toastr.error('Something went wrong');
                        });
                }
            }
        });
    });
}

