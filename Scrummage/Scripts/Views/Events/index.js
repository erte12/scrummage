var source = $('#eventRow').html();
var template = Handlebars.compile(source);
var table = $('#events');

function loadEventsTableData(teamId) {
    $.get('/Api/Events?teamId=' + teamId, function (data) {
        data.forEach(function (event) {
            var eventRowHtml = template(event);
            $('#events tbody').append(eventRowHtml);
        });
    }).done(function () {
        table = table.DataTable({
            "order": [2, 'desc']
        });
    });
}

function bindEventCreateEvent(teamId) {
    $('#newEventForm').validate({
        rules: {
            content: {
                required: true,
                minlength: 3,
                maxlength: 200
            },
            startsAt: {
                required: true,
                date: true
            },
            endsAt: {
                required: true,
                date: true
            }
        },
        submitHandler: function () {
            $.ajax({
                    url: '/Api/Events?teamId=' + teamId,
                    method: 'post',
                    data: $('#newEventForm').serialize()
                })
                .done(function (newEvent) {
                    var newEventRowHtml = template(newEvent);
                    table.row.add($(newEventRowHtml)).draw();
                    $('#newEventForm').find("input[type=text], textarea").val("");
                    $('#newEventModal').modal('toggle');
                    toastr.success('New event has been created!');
                })
                .fail(function (data) {
                    console.log(data);
                    toastr.error('Something went wrong!');
                });
            return false;
        }
    });
}

function bindDatePicker() {
    $('#starts').datetimepicker();
    $('#ends').datetimepicker({
        useCurrent: false //Important! See issue #1075
    });
    $("#starts").on("dp.change", function (e) {
        $('#ends').data("DateTimePicker").minDate(e.date);
    });
    $("#ends").on("dp.change", function (e) {
        $('#starts').data("DateTimePicker").maxDate(e.date);
    });
}

function bindEventDeleteEvent() {
    $('#events tbody').on('click', '.js-delete', function () {

        var eventRow = $(this).parents('tr');

        var eventId = eventRow.attr('data-event-id');

        bootbox.confirm({
            title: "Delete event?",
            message: "Do you want to delete this event? This cannot be undone.",
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
                            url: '/Api/Events/' + eventId,
                            method: 'delete'
                        })
                        .done(function () {
                            toastr.success('Event successfully deleted');
                            table.rows(eventRow).remove().draw();
                        })
                        .fail(function () {
                            toastr.error('Something went wrong');
                        });
                }
            }
        });
    });
}